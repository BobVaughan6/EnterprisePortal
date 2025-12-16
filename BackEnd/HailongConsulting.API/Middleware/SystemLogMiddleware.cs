using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using HailongConsulting.API.Models.DTOs;
using HailongConsulting.API.Services;

namespace HailongConsulting.API.Middleware;

/// <summary>
/// 系统日志中间件 - 自动记录所有API请求到数据库
/// </summary>
public class SystemLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<SystemLogMiddleware> _logger;

    public SystemLogMiddleware(
        RequestDelegate next, 
        ILogger<SystemLogMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, ISystemLogService systemLogService)
    {
        // 只记录API请求，排除静态文件和Swagger
        var path = context.Request.Path.Value?.ToLower() ?? "";
        if (path.StartsWith("/swagger") || 
            path.StartsWith("/api/systemlog") || // 避免记录日志查询本身
            path.Contains(".") || // 排除静态文件
            !path.StartsWith("/api"))
        {
            await _next(context);
            return;
        }

        // 检查是否需要记录
        if (!ShouldLogRequest(context))
        {
            await _next(context);
            return;
        }

        var stopwatch = Stopwatch.StartNew();
        var originalBodyStream = context.Response.Body;
        
        // 保存请求信息用于日志记录
        string requestBody = string.Empty;
        string responseBodyText = string.Empty;
        int statusCode = 200;
        bool hasError = false;
        Exception? caughtException = null;

        try
        {
            // 读取请求体
            requestBody = await ReadRequestBodyAsync(context.Request);

            // 使用内存流捕获响应
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            // 执行请求
            await _next(context);

            stopwatch.Stop();
            statusCode = context.Response.StatusCode;

            // 读取响应体
            responseBodyText = await ReadResponseBodyAsync(responseBody);

            // 复制响应到原始流
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            hasError = true;
            caughtException = ex;
            statusCode = 500;
            _logger.LogError(ex, "Error in SystemLogMiddleware");
            throw;
        }
        finally
        {
            context.Response.Body = originalBodyStream;
            
            // 在 finally 块中记录日志，确保无论成功还是失败都会记录
            try
            {
                if (hasError && caughtException != null)
                {
                    await LogErrorToDatabase(context, caughtException, stopwatch.ElapsedMilliseconds, systemLogService);
                }
                else
                {
                    await LogToDatabase(context, requestBody, responseBodyText, statusCode, stopwatch.ElapsedMilliseconds, systemLogService);
                }
            }
            catch (Exception logEx)
            {
                // 记录日志失败不应该影响主请求
                _logger.LogError(logEx, "Failed to log system log to database");
            }
        }
    }

    /// <summary>
    /// 判断是否需要记录该请求
    /// </summary>
    private bool ShouldLogRequest(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLower() ?? "";
        var method = context.Request.Method.ToUpper();
        var isAuthenticated = context.User?.Identity?.IsAuthenticated ?? false;

        // 匿名用户的查询操作不记录
        if (!isAuthenticated && method == "GET")
        {
            return false;
        }

        // 已登录用户的查询操作，只记录特定模块
        if (isAuthenticated && method == "GET")
        {
            // 只记录这些模块的查询操作
            return path.Contains("/announcement") ||      // 公告管理
                   path.Contains("/info-publication") || // 信息发布
                   path.Contains("/config") ||            // 系统配置
                   path.Contains("/user") ||              // 用户管理
                   path.Contains("/attachment");          // 附件管理
        }

        // 所有非查询操作都记录（POST/PUT/DELETE等）
        return true;
    }

    private async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        if (request.ContentLength == null || request.ContentLength == 0)
        {
            return string.Empty;
        }

        request.EnableBuffering();
        
        using var reader = new StreamReader(
            request.Body,
            encoding: Encoding.UTF8,
            detectEncodingFromByteOrderMarks: false,
            bufferSize: 1024,
            leaveOpen: true);

        var body = await reader.ReadToEndAsync();
        request.Body.Position = 0;

        // 限制长度，避免存储过大的数据
        return body.Length > 5000 ? body.Substring(0, 5000) + "..." : body;
    }

    private async Task<string> ReadResponseBodyAsync(MemoryStream responseBody)
    {
        responseBody.Seek(0, SeekOrigin.Begin);
        var text = await new StreamReader(responseBody).ReadToEndAsync();
        responseBody.Seek(0, SeekOrigin.Begin);

        // 限制长度
        return text.Length > 5000 ? text.Substring(0, 5000) + "..." : text;
    }

    private async Task LogToDatabase(
        HttpContext context, 
        string requestBody, 
        string responseBody,
        int statusCode,
        long elapsedMilliseconds,
        ISystemLogService systemLogService)
    {
        var user = context.User;
        var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username = user?.FindFirst(ClaimTypes.Name)?.Value ?? "Anonymous";

        var action = GetActionFromPath(context.Request.Path, context.Request.Method);
        var module = GetModuleFromPath(context.Request.Path);

        var logDto = new CreateSystemLogDto
        {
            UserId = string.IsNullOrEmpty(userId) ? null : int.Parse(userId),
            Username = username,
            Action = action,
            Module = module,
            Description = $"{context.Request.Method} {context.Request.Path} - {statusCode} ({elapsedMilliseconds}ms)",
            IpAddress = GetClientIpAddress(context),
            UserAgent = context.Request.Headers["User-Agent"].ToString(),
            RequestData = string.IsNullOrEmpty(requestBody) ? null : requestBody,
            ResponseData = statusCode >= 400 ? responseBody : null, // 只记录错误响应
            Status = statusCode < 400 ? "Success" : "Failed"
        };

        await systemLogService.CreateAsync(logDto);
    }

    private async Task LogErrorToDatabase(
        HttpContext context,
        Exception exception,
        long elapsedMilliseconds,
        ISystemLogService systemLogService)
    {
        var user = context.User;
        var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username = user?.FindFirst(ClaimTypes.Name)?.Value ?? "Anonymous";

        var logDto = new CreateSystemLogDto
        {
            UserId = string.IsNullOrEmpty(userId) ? null : int.Parse(userId),
            Username = username,
            Action = "ERROR",
            Module = GetModuleFromPath(context.Request.Path),
            Description = $"Exception: {exception.Message}",
            IpAddress = GetClientIpAddress(context),
            UserAgent = context.Request.Headers["User-Agent"].ToString(),
            RequestData = $"Path: {context.Request.Path}, Method: {context.Request.Method}",
            ResponseData = exception.StackTrace,
            Status = "Error"
        };

        await systemLogService.CreateAsync(logDto);
    }

    private string GetActionFromPath(PathString path, string method)
    {
        var pathValue = path.Value?.ToLower() ?? "";
        
        if (pathValue.Contains("/auth/login")) return "LOGIN";
        if (pathValue.Contains("/auth/logout")) return "LOGOUT";
        
        return method.ToUpper() switch
        {
            "GET" => "QUERY",
            "POST" => "CREATE",
            "PUT" => "UPDATE",
            "DELETE" => "DELETE",
            _ => method.ToUpper()
        };
    }

    private string GetModuleFromPath(PathString path)
    {
        var pathValue = path.Value?.ToLower() ?? "";
        
        if (pathValue.Contains("/auth")) return "认证";
        if (pathValue.Contains("/user")) return "用户管理";
        if (pathValue.Contains("/announcement")) return "公告管理";
        if (pathValue.Contains("/info-publication")) return "信息发布";  // 匹配 /info-publications
        if (pathValue.Contains("/attachment")) return "附件管理";
        if (pathValue.Contains("/config")) return "系统配置";
        if (pathValue.Contains("/statistics")) return "统计分析";
        if (pathValue.Contains("/region")) return "区域字典";
        if (pathValue.Contains("/search")) return "全局搜索";
        
        return "其他";
    }

    private string GetClientIpAddress(HttpContext context)
    {
        var ipAddress = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (string.IsNullOrEmpty(ipAddress))
        {
            ipAddress = context.Request.Headers["X-Real-IP"].FirstOrDefault();
        }
        if (string.IsNullOrEmpty(ipAddress))
        {
            ipAddress = context.Connection.RemoteIpAddress?.ToString();
        }
        return ipAddress ?? "Unknown";
    }
}

/// <summary>
/// 系统日志中间件扩展方法
/// </summary>
public static class SystemLogMiddlewareExtensions
{
    public static IApplicationBuilder UseSystemLog(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SystemLogMiddleware>();
    }
}