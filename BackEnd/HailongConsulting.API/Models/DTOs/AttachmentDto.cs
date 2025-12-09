namespace HailongConsulting.API.Models.DTOs;

/// <summary>
/// 附件DTO
/// </summary>
public class AttachmentDto
{
    /// <summary>
    /// 附件ID
    /// </summary>
    public uint Id { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// 文件路径
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// 文件访问URL
    /// </summary>
    public string FileUrl { get; set; } = string.Empty;

    /// <summary>
    /// 文件大小（字节）
    /// </summary>
    public long? FileSize { get; set; }

    /// <summary>
    /// 文件类型（MIME类型）
    /// </summary>
    public string? FileType { get; set; }

    /// <summary>
    /// 文件扩展名
    /// </summary>
    public string? FileExtension { get; set; }

    /// <summary>
    /// 附件分类：image-图片, document-文档, video-视频, other-其他
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// 关联类型
    /// </summary>
    public string? RelatedType { get; set; }

    /// <summary>
    /// 关联记录ID
    /// </summary>
    public uint? RelatedId { get; set; }

    /// <summary>
    /// 上传用户ID
    /// </summary>
    public uint? UploadUserId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// 附件上传DTO
/// </summary>
public class UploadAttachmentDto
{
    public string? Category { get; set; }
    public string? RelatedType { get; set; }
    public uint? RelatedId { get; set; }
}