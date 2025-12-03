using System.ComponentModel.DataAnnotations;

namespace HailongConsulting.API.Models.DTOs;

/// <summary>
/// 建设工程公告DTO
/// </summary>
public class ConstructionProjectDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string NoticeType { get; set; } = string.Empty;
    public string? Bidder { get; set; }
    public string? Winner { get; set; }
    public string ProjectRegion { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? Publisher { get; set; }
    public DateTime? PublishTime { get; set; }
    public int ViewCount { get; set; }
    public string? AttachmentPath { get; set; }
    public bool IsTop { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// 创建建设工程公告DTO
/// </summary>
public class CreateConstructionProjectDto
{
    [Required(ErrorMessage = "标题不能为空")]
    [MaxLength(255, ErrorMessage = "标题长度不能超过255个字符")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "公告类型不能为空")]
    [RegularExpression("^(招标公告|中标公告|变更公告)$", ErrorMessage = "公告类型必须是：招标公告、中标公告或变更公告")]
    public string Type { get; set; } = string.Empty;

    [Required(ErrorMessage = "公告内容不能为空")]
    public string Content { get; set; } = string.Empty;

    [MaxLength(255, ErrorMessage = "招标人名称长度不能超过255个字符")]
    public string? Tenderer { get; set; }

    [MaxLength(255, ErrorMessage = "中标人名称长度不能超过255个字符")]
    public string? Winner { get; set; }

    [Required(ErrorMessage = "项目区域不能为空")]
    [MaxLength(50, ErrorMessage = "项目区域长度不能超过50个字符")]
    public string Region { get; set; } = string.Empty;

    [Required(ErrorMessage = "发布日期不能为空")]
    public DateTime PublishDate { get; set; }
}

/// <summary>
/// 更新建设工程公告DTO
/// </summary>
public class UpdateConstructionProjectDto
{
    [MaxLength(255, ErrorMessage = "标题长度不能超过255个字符")]
    public string? Title { get; set; }

    [RegularExpression("^(招标公告|中标公告|变更公告)$", ErrorMessage = "公告类型必须是：招标公告、中标公告或变更公告")]
    public string? Type { get; set; }

    public string? Content { get; set; }

    [MaxLength(255, ErrorMessage = "招标人名称长度不能超过255个字符")]
    public string? Tenderer { get; set; }

    [MaxLength(255, ErrorMessage = "中标人名称长度不能超过255个字符")]
    public string? Winner { get; set; }

    [MaxLength(50, ErrorMessage = "项目区域长度不能超过50个字符")]
    public string? Region { get; set; }

    public DateTime? PublishDate { get; set; }
}

/// <summary>
/// 建设工程公告查询ViewModel
/// </summary>
public class ConstructionProjectQueryViewModel
{
    /// <summary>
    /// 页码
    /// </summary>
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// 每页数量
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// 关键词（搜索标题、招标人、中标人）
    /// </summary>
    public string? Keyword { get; set; }

    /// <summary>
    /// 公告类型
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// 地区（支持多个，逗号分隔）
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 排序字段
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// 是否降序
    /// </summary>
    public bool IsDescending { get; set; } = true;
}