using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HailongConsulting.API.Models.Entities;

/// <summary>
/// 建设工程公告实体
/// </summary>
[Table("construction_project_notices")]
public class ConstructionProjectAnnouncement
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [Column("notice_type")]
    public string NoticeType { get; set; } = string.Empty; // 招标公告/中标公告/变更公告

    [MaxLength(255)]
    [Column("bidder")]
    public string? Bidder { get; set; } // 招标人

    [MaxLength(255)]
    [Column("winner")]
    public string? Winner { get; set; } // 中标人

    [Required]
    [MaxLength(50)]
    [Column("project_region")]
    public string ProjectRegion { get; set; } = string.Empty; // 项目区域

    [Required]
    [Column("content", TypeName = "longtext")]
    public string Content { get; set; } = string.Empty; // 公告内容（富文本）

    [MaxLength(50)]
    [Column("publisher")]
    public string? Publisher { get; set; } // 发布人

    [Column("publish_time")]
    public DateTime? PublishTime { get; set; } // 发布时间

    [Column("view_count")]
    public int ViewCount { get; set; } = 0;

    [MaxLength(500)]
    [Column("attachment_path")]
    public string? AttachmentPath { get; set; } // 附件路径

    [Column("is_top")]
    public bool IsTop { get; set; } = false; // 是否置顶

    [Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}