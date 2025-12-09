using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HailongConsulting.API.Models.Entities;

/// <summary>
/// 项目区域字典实体（省市区三级结构）
/// </summary>
[Table("region_dictionary")]
public class RegionDictionary
{
    /// <summary>
    /// 区域ID
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 区域代码（如：410000、410100、410101）
    /// </summary>
    [Column("region_code")]
    [Required]
    [MaxLength(50)]
    public string RegionCode { get; set; } = string.Empty;

    /// <summary>
    /// 区域名称
    /// </summary>
    [Column("region_name")]
    [Required]
    [MaxLength(50)]
    public string RegionName { get; set; } = string.Empty;

    /// <summary>
    /// 区域级别：1-省份，2-城市，3-区县
    /// </summary>
    [Column("region_level")]
    public sbyte RegionLevel { get; set; }

    /// <summary>
    /// 父级区域代码
    /// </summary>
    [Column("parent_code")]
    [MaxLength(50)]
    public string? ParentCode { get; set; }

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