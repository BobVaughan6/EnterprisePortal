using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HailongConsulting.API.Models.Entities;

/// <summary>
/// 友情链接实体
/// </summary>
[Table("friendly_links")]
public class FriendlyLink
{
    /// <summary>
    /// 链接ID
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 链接名称
    /// </summary>
    [Column("link_name")]
    [Required]
    [MaxLength(100)]
    public string LinkName { get; set; } = string.Empty;

    /// <summary>
    /// 链接URL
    /// </summary>
    [Column("link_url")]
    [Required]
    [MaxLength(500)]
    public string LinkUrl { get; set; } = string.Empty;

    /// <summary>
    /// Logo URL
    /// </summary>
    [Column("logo_url")]
    [MaxLength(500)]
    public string? LogoUrl { get; set; }

    /// <summary>
    /// 排序顺序
    /// </summary>
    [Column("sort_order")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 状态：0-禁用，1-启用
    /// </summary>
    [Column("status")]
    public sbyte Status { get; set; } = 1;

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