using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HailongConsulting.API.Models.Entities;

/// <summary>
/// 角色权限实体
/// </summary>
[Table("admin_roles")]
public class AdminRole
{
    /// <summary>
    /// 角色ID
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    [Column("role_name")]
    [Required]
    [MaxLength(50)]
    public string RoleName { get; set; } = string.Empty;

    /// <summary>
    /// 角色代码
    /// </summary>
    [Column("role_code")]
    [Required]
    [MaxLength(50)]
    public string RoleCode { get; set; } = string.Empty;

    /// <summary>
    /// 角色描述
    /// </summary>
    [Column("description")]
    [MaxLength(255)]
    public string? Description { get; set; }

    /// <summary>
    /// 权限列表（JSON格式）
    /// </summary>
    [Column("permissions")]
    public string? Permissions { get; set; }

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