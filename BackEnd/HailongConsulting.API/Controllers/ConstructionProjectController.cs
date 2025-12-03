using HailongConsulting.API.Common;
using HailongConsulting.API.Models.DTOs;
using HailongConsulting.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HailongConsulting.API.Controllers;

/// <summary>
/// 建设工程公告控制器
/// </summary>
[ApiController]
[Route("api/construction/announcements")]
public class ConstructionProjectController : ControllerBase
{
    private readonly IConstructionProjectService _service;
    private readonly ILogger<ConstructionProjectController> _logger;

    public ConstructionProjectController(IConstructionProjectService service, ILogger<ConstructionProjectController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// 获取公告列表（分页、支持多条件搜索）
    /// </summary>
    /// <param name="query">查询参数</param>
    /// <returns>分页结果</returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<PagedResult<ConstructionProjectDto>>>> GetAnnouncements([FromQuery] ConstructionProjectQueryViewModel query)
    {
        var result = await _service.GetAnnouncementsAsync(query);
        return Ok(ApiResponse<PagedResult<ConstructionProjectDto>>.SuccessResult(result, "获取成功"));
    }

    /// <summary>
    /// 根据ID获取公告详情（自动增加访问量）
    /// </summary>
    /// <param name="id">公告ID</param>
    /// <returns>公告详情</returns>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<ConstructionProjectDto>>> GetAnnouncement(int id)
    {
        var announcement = await _service.GetAnnouncementByIdAsync(id);

        if (announcement == null)
        {
            return NotFound(ApiResponse<ConstructionProjectDto>.FailResult("公告不存在"));
        }

        // 增加浏览次数
        await _service.IncrementViewCountAsync(id);

        return Ok(ApiResponse<ConstructionProjectDto>.SuccessResult(announcement, "获取成功"));
    }

    /// <summary>
    /// 创建公告（需认证）
    /// </summary>
    /// <param name="dto">创建公告DTO</param>
    /// <returns>创建的公告</returns>
    [HttpPost]
    [Authorize(Roles = "admin,manager")]
    public async Task<ActionResult<ApiResponse<ConstructionProjectDto>>> CreateAnnouncement([FromBody] CreateConstructionProjectDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<ConstructionProjectDto>.FailResult("请求参数无效"));
        }

        var announcement = await _service.CreateAnnouncementAsync(dto);
        _logger.LogInformation("Construction project announcement {Title} created", dto.Title);

        return CreatedAtAction(
            nameof(GetAnnouncement),
            new { id = announcement.Id },
            ApiResponse<ConstructionProjectDto>.SuccessResult(announcement, "创建成功")
        );
    }

    /// <summary>
    /// 更新公告（需认证）
    /// </summary>
    /// <param name="id">公告ID</param>
    /// <param name="dto">更新公告DTO</param>
    /// <returns>更新后的公告</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "admin,manager")]
    public async Task<ActionResult<ApiResponse<ConstructionProjectDto>>> UpdateAnnouncement(int id, [FromBody] UpdateConstructionProjectDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<ConstructionProjectDto>.FailResult("请求参数无效"));
        }

        var announcement = await _service.UpdateAnnouncementAsync(id, dto);

        if (announcement == null)
        {
            return NotFound(ApiResponse<ConstructionProjectDto>.FailResult("公告不存在"));
        }

        _logger.LogInformation("Construction project announcement {Id} updated", id);
        return Ok(ApiResponse<ConstructionProjectDto>.SuccessResult(announcement, "更新成功"));
    }

    /// <summary>
    /// 删除公告（软删除，需认证）
    /// </summary>
    /// <param name="id">公告ID</param>
    /// <returns>删除结果</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<ApiResponse>> DeleteAnnouncement(int id)
    {
        var result = await _service.DeleteAnnouncementAsync(id);

        if (!result)
        {
            return NotFound(ApiResponse.FailResponse("公告不存在"));
        }

        _logger.LogInformation("Construction project announcement {Id} deleted", id);
        return Ok(ApiResponse.SuccessResponse("删除成功"));
    }
}