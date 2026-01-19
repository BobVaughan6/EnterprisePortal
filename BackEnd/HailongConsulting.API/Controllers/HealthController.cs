using Microsoft.AspNetCore.Mvc;

namespace HailongConsulting.API.Controllers;

/// <summary>
/// 健康检查控制器
/// </summary>
[ApiController]
[Route("")]
public class HealthController : ControllerBase
{
    /// <summary>
    /// 健康检查端点
    /// </summary>
    /// <returns>健康状态</returns>
    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
    }

    /// <summary>
    /// 根路径健康检查
    /// </summary>
    /// <returns>健康状态</returns>
    [HttpGet("")]
    public IActionResult Root()
    {
        return Ok(new { message = "Hailong Consulting API is running", version = "1.0.0" });
    }
}
