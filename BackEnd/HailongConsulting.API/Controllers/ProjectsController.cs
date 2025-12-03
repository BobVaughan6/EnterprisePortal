using HailongConsulting.API.Common;
using HailongConsulting.API.Models.DTOs;
using HailongConsulting.API.Models.ViewModels;
using HailongConsulting.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HailongConsulting.API.Controllers;

/// <summary>
/// 项目控制器
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(IProjectService projectService, ILogger<ProjectsController> logger)
    {
        _projectService = projectService;
        _logger = logger;
    }

    /// <summary>
    /// 获取项目列表（分页）
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<PagedResult<ProjectDto>>>> GetProjects([FromQuery] ProjectQueryViewModel query)
    {
        var result = await _projectService.GetProjectsAsync(query);
        return Ok(ApiResponse<PagedResult<ProjectDto>>.SuccessResult(result, "获取成功"));
    }

    /// <summary>
    /// 根据ID获取项目详情
    /// </summary>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<ProjectDto>>> GetProject(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);

        if (project == null)
        {
            return NotFound(ApiResponse<ProjectDto>.FailResult("项目不存在"));
        }

        // 增加浏览次数
        await _projectService.IncrementViewCountAsync(id);

        return Ok(ApiResponse<ProjectDto>.SuccessResult(project, "获取成功"));
    }

    /// <summary>
    /// 创建项目
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "admin,manager")]
    public async Task<ActionResult<ApiResponse<ProjectDto>>> CreateProject([FromBody] CreateProjectDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<ProjectDto>.FailResult("请求参数无效"));
        }

        var project = await _projectService.CreateProjectAsync(dto);
        _logger.LogInformation("Project {ProjectCode} created", dto.ProjectCode);

        return CreatedAtAction(
            nameof(GetProject),
            new { id = project.ProjectId },
            ApiResponse<ProjectDto>.SuccessResult(project, "创建成功")
        );
    }

    /// <summary>
    /// 更新项目
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "admin,manager")]
    public async Task<ActionResult<ApiResponse<ProjectDto>>> UpdateProject(int id, [FromBody] UpdateProjectDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<ProjectDto>.FailResult("请求参数无效"));
        }

        var project = await _projectService.UpdateProjectAsync(id, dto);

        if (project == null)
        {
            return NotFound(ApiResponse<ProjectDto>.FailResult("项目不存在"));
        }

        _logger.LogInformation("Project {ProjectId} updated", id);
        return Ok(ApiResponse<ProjectDto>.SuccessResult(project, "更新成功"));
    }

    /// <summary>
    /// 删除项目
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<ApiResponse>> DeleteProject(int id)
    {
        var result = await _projectService.DeleteProjectAsync(id);

        if (!result)
        {
            return NotFound(ApiResponse.FailResponse("项目不存在"));
        }

        _logger.LogInformation("Project {ProjectId} deleted", id);
        return Ok(ApiResponse.SuccessResponse("删除成功"));
    }
}