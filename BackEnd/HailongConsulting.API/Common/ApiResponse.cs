namespace HailongConsulting.API.Common;

/// <summary>
/// 统一API响应格式
/// </summary>
/// <typeparam name="T">数据类型</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 响应消息
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// 响应数据
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.Now;

    /// <summary>
    /// 成功响应
    /// </summary>
    public static ApiResponse<T> SuccessResult(T data, string message = "操作成功")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data,
            Timestamp = DateTime.Now
        };
    }

    /// <summary>
    /// 失败响应
    /// </summary>
    public static ApiResponse<T> FailResult(string message = "操作失败")
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Data = default,
            Timestamp = DateTime.Now
        };
    }
}

/// <summary>
/// 无数据的统一响应
/// </summary>
public class ApiResponse
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 响应消息
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// 时间戳
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.Now;

    public static ApiResponse SuccessResponse(string message = "操作成功")
    {
        return new ApiResponse
        {
            Success = true,
            Message = message,
            Timestamp = DateTime.Now
        };
    }

    public static ApiResponse FailResponse(string message = "操作失败")
    {
        return new ApiResponse
        {
            Success = false,
            Message = message,
            Timestamp = DateTime.Now
        };
    }
}