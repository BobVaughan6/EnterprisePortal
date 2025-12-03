using HailongConsulting.API.Common;
using HailongConsulting.API.Models.DTOs;
using HailongConsulting.API.Models.ViewModels;

namespace HailongConsulting.API.Services;

/// <summary>
/// 项目服务接口
/// </summary>
public interface IProjectService
{
    Task<PagedResult<ProjectDto>> GetProjectsAsync(ProjectQueryViewModel query);
    Task<ProjectDto?> GetProjectByIdAsync(int id);
    Task<ProjectDto> CreateProjectAsync(CreateProjectDto dto);
    Task<ProjectDto?> UpdateProjectAsync(int id, UpdateProjectDto dto);
    Task<bool> DeleteProjectAsync(int id);
    Task<bool> IncrementViewCountAsync(int id);
}