using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HailongConsulting.API.Models.Entities;

/// <summary>
/// 重要业绩实体
/// </summary>
[Table("major_achievements")]
public class MajorAchievement
{
    /// <summary>
    /// 业绩ID
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 项目名称
    /// </summary>
    [Column("project_name")]
    [Required]
    [MaxLength(255)]
    public string ProjectName { get; set; } = string.Empty;

    /// <summary>
    /// 项目类型
    /// </summary>
    [Column("project_type")]
    [MaxLength(50)]
    public string? ProjectType { get; set; }

    /// <summary>
    /// 项目金额（万元）
    /// </summary>
    [Column("project_amount")]
    [Precision(15, 2)]
    public decimal? ProjectAmount { get; set; }

    /// <summary>
    /// 客户名称
    /// </summary>
    [Column("client_name")]
    [MaxLength(255)]
    public string? ClientName { get; set; }

    /// <summary>
    /// 完成日期
    /// </summary>
    [Column("completion_date")]
    public DateOnly? CompletionDate { get; set; }

    /// <summary>
    /// 项目描述
    /// </summary>
    [Column("description")]
    public string? Description { get; set; }

    /// <summary>
    /// 项目图片URL
    /// </summary>
    [Column("image_url")]
    [MaxLength(500)]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// 排序顺序
    /// </summary>
    [Column("sort_order")]
    public int SortOrder { get; set; } = 0;

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