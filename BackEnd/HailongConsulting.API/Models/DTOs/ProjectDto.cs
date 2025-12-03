using System.ComponentModel.DataAnnotations;

namespace HailongConsulting.API.Models.DTOs;

/// <summary>
/// 项目创建DTO
/// </summary>
public class CreateProjectDto
{
    [Required(ErrorMessage = "项目名称不能为空")]
    [MaxLength(200)]
    public string ProjectName { get; set; } = string.Empty;

    [Required(ErrorMessage = "项目代码不能为空")]
    [MaxLength(50)]
    public string ProjectCode { get; set; } = string.Empty;

    public int? ClientId { get; set; }
    public int? CategoryId { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; } = "planning";
    public decimal? Budget { get; set; }
    public int? ProjectManagerId { get; set; }
    public string? CoverImage { get; set; }
    public List<string>? Tags { get; set; }
    public bool IsFeatured { get; set; } = false;
}

/// <summary>
/// 项目更新DTO
/// </summary>
public class UpdateProjectDto
{
    [MaxLength(200)]
    public string? ProjectName { get; set; }
    
    public int? ClientId { get; set; }
    public int? CategoryId { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Status { get; set; }
    public decimal? Budget { get; set; }
    public decimal? ActualCost { get; set; }
    public int? ProjectManagerId { get; set; }
    public string? CoverImage { get; set; }
    public List<string>? Tags { get; set; }
    public bool? IsFeatured { get; set; }
}

/// <summary>
/// 项目响应DTO
/// </summary>
public class ProjectDto
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string ProjectCode { get; set; } = string.Empty;
    public int? ClientId { get; set; }
    public string? ClientName { get; set; }
    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal? Budget { get; set; }
    public decimal? ActualCost { get; set; }
    public int? ProjectManagerId { get; set; }
    public string? ProjectManagerName { get; set; }
    public string? CoverImage { get; set; }
    public List<string>? Tags { get; set; }
    public bool IsFeatured { get; set; }
    public int ViewCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}