using AutoMapper;
using HailongConsulting.API.Common;
using HailongConsulting.API.Data;
using HailongConsulting.API.Models.DTOs;
using HailongConsulting.API.Models.Entities;
using HailongConsulting.API.Models.ViewModels;
using HailongConsulting.API.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace HailongConsulting.API.Services;

/// <summary>
/// 项目服务实现
/// </summary>
public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public ProjectService(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext context)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _context = context;
    }

    public async Task<PagedResult<ProjectDto>> GetProjectsAsync(ProjectQueryViewModel query)
    {
        var projectsQuery = _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Category)
            .Include(p => p.ProjectManager)
            .AsQueryable();

        // 应用过滤条件
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            projectsQuery = projectsQuery.Where(p => 
                p.ProjectName.Contains(query.Keyword) || 
                p.Description!.Contains(query.Keyword));
        }

        if (query.ClientId.HasValue)
        {
            projectsQuery = projectsQuery.Where(p => p.ClientId == query.ClientId.Value);
        }

        if (query.CategoryId.HasValue)
        {
            projectsQuery = projectsQuery.Where(p => p.CategoryId == query.CategoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.Status))
        {
            projectsQuery = projectsQuery.Where(p => p.Status == query.Status);
        }

        if (query.IsFeatured.HasValue)
        {
            projectsQuery = projectsQuery.Where(p => p.IsFeatured == query.IsFeatured.Value);
        }

        if (query.StartDateFrom.HasValue)
        {
            projectsQuery = projectsQuery.Where(p => p.StartDate >= query.StartDateFrom.Value);
        }

        if (query.StartDateTo.HasValue)
        {
            projectsQuery = projectsQuery.Where(p => p.StartDate <= query.StartDateTo.Value);
        }

        // 获取总数
        var totalCount = await projectsQuery.CountAsync();

        // 排序
        projectsQuery = query.SortBy?.ToLower() switch
        {
            "projectname" => query.IsDescending 
                ? projectsQuery.OrderByDescending(p => p.ProjectName)
                : projectsQuery.OrderBy(p => p.ProjectName),
            "startdate" => query.IsDescending
                ? projectsQuery.OrderByDescending(p => p.StartDate)
                : projectsQuery.OrderBy(p => p.StartDate),
            "viewcount" => query.IsDescending
                ? projectsQuery.OrderByDescending(p => p.ViewCount)
                : projectsQuery.OrderBy(p => p.ViewCount),
            _ => query.IsDescending
                ? projectsQuery.OrderByDescending(p => p.CreatedAt)
                : projectsQuery.OrderBy(p => p.CreatedAt)
        };

        // 分页
        var projects = await projectsQuery
            .Skip((query.PageIndex - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        // 映射到DTO
        var projectDtos = projects.Select(p => new ProjectDto
        {
            ProjectId = p.ProjectId,
            ProjectName = p.ProjectName,
            ProjectCode = p.ProjectCode,
            ClientId = p.ClientId,
            ClientName = p.Client?.ClientName,
            CategoryId = p.CategoryId,
            CategoryName = p.Category?.CategoryName,
            Description = p.Description,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
            Status = p.Status,
            Budget = p.Budget,
            ActualCost = p.ActualCost,
            ProjectManagerId = p.ProjectManagerId,
            ProjectManagerName = p.ProjectManager?.FullName,
            CoverImage = p.CoverImage,
            Tags = string.IsNullOrEmpty(p.Tags) ? null : JsonSerializer.Deserialize<List<string>>(p.Tags),
            IsFeatured = p.IsFeatured,
            ViewCount = p.ViewCount,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        }).ToList();

        return PagedResult<ProjectDto>.Create(projectDtos, totalCount, query.PageIndex, query.PageSize);
    }

    public async Task<ProjectDto?> GetProjectByIdAsync(int id)
    {
        var project = await _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Category)
            .Include(p => p.ProjectManager)
            .FirstOrDefaultAsync(p => p.ProjectId == id);

        if (project == null)
        {
            return null;
        }

        return new ProjectDto
        {
            ProjectId = project.ProjectId,
            ProjectName = project.ProjectName,
            ProjectCode = project.ProjectCode,
            ClientId = project.ClientId,
            ClientName = project.Client?.ClientName,
            CategoryId = project.CategoryId,
            CategoryName = project.Category?.CategoryName,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Status = project.Status,
            Budget = project.Budget,
            ActualCost = project.ActualCost,
            ProjectManagerId = project.ProjectManagerId,
            ProjectManagerName = project.ProjectManager?.FullName,
            CoverImage = project.CoverImage,
            Tags = string.IsNullOrEmpty(project.Tags) ? null : JsonSerializer.Deserialize<List<string>>(project.Tags),
            IsFeatured = project.IsFeatured,
            ViewCount = project.ViewCount,
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt
        };
    }

    public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto dto)
    {
        var project = new Project
        {
            ProjectName = dto.ProjectName,
            ProjectCode = dto.ProjectCode,
            ClientId = dto.ClientId,
            CategoryId = dto.CategoryId,
            Description = dto.Description,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Status = dto.Status,
            Budget = dto.Budget,
            ProjectManagerId = dto.ProjectManagerId,
            CoverImage = dto.CoverImage,
            Tags = dto.Tags != null ? JsonSerializer.Serialize(dto.Tags) : null,
            IsFeatured = dto.IsFeatured
        };

        await _unitOfWork.Projects.AddAsync(project);
        await _unitOfWork.SaveChangesAsync();

        return (await GetProjectByIdAsync(project.ProjectId))!;
    }

    public async Task<ProjectDto?> UpdateProjectAsync(int id, UpdateProjectDto dto)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(id);
        
        if (project == null)
        {
            return null;
        }

        // 更新字段
        if (!string.IsNullOrWhiteSpace(dto.ProjectName))
            project.ProjectName = dto.ProjectName;
        
        if (dto.ClientId.HasValue)
            project.ClientId = dto.ClientId;
        
        if (dto.CategoryId.HasValue)
            project.CategoryId = dto.CategoryId;
        
        if (dto.Description != null)
            project.Description = dto.Description;
        
        if (dto.StartDate.HasValue)
            project.StartDate = dto.StartDate;
        
        if (dto.EndDate.HasValue)
            project.EndDate = dto.EndDate;
        
        if (!string.IsNullOrWhiteSpace(dto.Status))
            project.Status = dto.Status;
        
        if (dto.Budget.HasValue)
            project.Budget = dto.Budget;
        
        if (dto.ActualCost.HasValue)
            project.ActualCost = dto.ActualCost;
        
        if (dto.ProjectManagerId.HasValue)
            project.ProjectManagerId = dto.ProjectManagerId;
        
        if (dto.CoverImage != null)
            project.CoverImage = dto.CoverImage;
        
        if (dto.Tags != null)
            project.Tags = JsonSerializer.Serialize(dto.Tags);
        
        if (dto.IsFeatured.HasValue)
            project.IsFeatured = dto.IsFeatured.Value;

        _unitOfWork.Projects.Update(project);
        await _unitOfWork.SaveChangesAsync();

        return await GetProjectByIdAsync(id);
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(id);
        
        if (project == null)
        {
            return false;
        }

        _unitOfWork.Projects.Remove(project);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> IncrementViewCountAsync(int id)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(id);
        
        if (project == null)
        {
            return false;
        }

        project.ViewCount++;
        _unitOfWork.Projects.Update(project);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}