using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HailongConsulting.API.Models.Entities;

/// <summary>
/// 附件实体（统一管理所有附件和图片）
/// </summary>
[Table("attachments")]
public class Attachment
{
    /// <summary>
    /// 附件ID
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    [Column("file_name")]
    [Required]
    [MaxLength(255)]
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// 文件路径
    /// </summary>
    [Column("file_path")]
    [Required]
    [MaxLength(500)]
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// 文件访问URL
    /// </summary>
    [Column("file_url")]
    [Required]
    [MaxLength(500)]
    public string FileUrl { get; set; } = string.Empty;

    /// <summary>
    /// 文件大小（字节）
    /// </summary>
    [Column("file_size")]
    public long? FileSize { get; set; }

    /// <summary>
    /// 文件类型（MIME类型）
    /// </summary>
    [Column("file_type")]
    [MaxLength(50)]
    public string? FileType { get; set; }

    /// <summary>
    /// 文件扩展名
    /// </summary>
    [Column("file_extension")]
    [MaxLength(20)]
    public string? FileExtension { get; set; }

    /// <summary>
    /// 附件分类：image-图片, document-文档, video-视频, other-其他
    /// </summary>
    [Column("category")]
    [MaxLength(50)]
    public string? Category { get; set; }

    /// <summary>
    /// 关联类型：announcement-公告, info_publication-信息发布, company_profile-企业简介等
    /// </summary>
    [Column("related_type")]
    [MaxLength(50)]
    public string? RelatedType { get; set; }

    /// <summary>
    /// 关联记录ID
    /// </summary>
    [Column("related_id")]
    public uint? RelatedId { get; set; }

    /// <summary>
    /// 上传用户ID
    /// </summary>
    [Column("upload_user_id")]
    public uint? UploadUserId { get; set; }

    /// <summary>
    /// 软删除：0-未删除，1-已删除
    /// </summary>
    [Column("is_deleted")]
    public sbyte IsDeleted { get; set; } = 0;

    /// <summary>
    /// 创建时间
    /// </summary>
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// 更新时间
    /// </summary>
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}