namespace HailongConsulting.API.Models.DTOs;

/// <summary>
/// 统一信息发布DTO
/// </summary>
public class InfoPublicationDto
{
    /// <summary>
    /// 信息ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 信息类型：COMPANY_NEWS-新闻中心, POLICY_REGULATION-政策法规
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// 二级分类
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 摘要
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// 内容（富文本）
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 封面图片ID
    /// </summary>
    public int? CoverImageId { get; set; }

    /// <summary>
    /// 封面图片信息
    /// </summary>
    public AttachmentDto? CoverImage { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// 发布人
    /// </summary>
    public string? Publisher { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

    /// <summary>
    /// 浏览次数
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// 附件ID列表
    /// </summary>
    public List<int>? AttachmentIds { get; set; }

    /// <summary>
    /// 附件列表
    /// </summary>
    public List<AttachmentDto>? Attachments { get; set; }

    /// <summary>
    /// 是否置顶
    /// </summary>
    public bool IsTop { get; set; }

    /// <summary>
    /// 状态：0-禁用，1-启用
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// 信息发布创建DTO
/// </summary>
public class CreateInfoPublicationDto
{
    public string Type { get; set; } = string.Empty;
    public string? Category { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public string Content { get; set; } = string.Empty;
    public int? CoverImageId { get; set; }
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public DateTime? PublishTime { get; set; }
    public List<int>? AttachmentIds { get; set; }
    public bool IsTop { get; set; }
    public int Status { get; set; } = 1;
}

/// <summary>
/// 信息发布更新DTO
/// </summary>
public class UpdateInfoPublicationDto
{
    public string? Category { get; set; }
    public string? Title { get; set; }
    public string? Summary { get; set; }
    public string? Content { get; set; }
    public int? CoverImageId { get; set; }
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public DateTime? PublishTime { get; set; }
    public List<int>? AttachmentIds { get; set; }
    public bool? IsTop { get; set; }
    public int? Status { get; set; }
}

/// <summary>
/// 信息发布查询DTO
/// </summary>
public class InfoPublicationQueryDto
{
    public string? Type { get; set; }
    public string? Category { get; set; }
    public string? Keyword { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; } = "PublishTime";
    public string? SortOrder { get; set; } = "desc";
}