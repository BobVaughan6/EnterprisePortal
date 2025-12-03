using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HailongConsulting.API.Models.Entities;

/// <summary>
/// 企业简介实体
/// </summary>
[Table("company_profile")]
public class CompanyProfile
{
    /// <summary>
    /// 简介ID
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 简介标题
    /// </summary>
    [Column("title")]
    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 简介内容（富文本）
    /// </summary>
    [Column("content")]
    [Required]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 配图URL
    /// </summary>
    [Column("image_url")]
    [MaxLength(500)]
    public string? ImageUrl { get; set; }

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