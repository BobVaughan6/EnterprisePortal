using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HailongConsulting.API.Models.Entities;

/// <summary>
/// 管理员账号实体
/// </summary>
[Table("admin_users")]
public class AdminUser
{
    /// <summary>
    /// 管理员ID
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [Column("username")]
    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// 密码（加密存储）
    /// </summary>
    [Column("password")]
    [Required]
    [MaxLength(255)]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 真实姓名
    /// </summary>
    [Column("real_name")]
    [MaxLength(50)]
    public string? RealName { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [Column("email")]
    [MaxLength(100)]
    public string? Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    [Column("phone")]
    [MaxLength(20)]
    public string? Phone { get; set; }

    /// <summary>
    /// 角色ID
    /// </summary>
    [Column("role_id")]
    public uint? RoleId { get; set; }

    /// <summary>
    /// 状态：0-禁用，1-启用
    /// </summary>
    [Column("status")]
    public sbyte Status { get; set; } = 1;

    /// <summary>
    /// 最后登录时间
    /// </summary>
    [Column("last_login_time")]
    public DateTime? LastLoginTime { get; set; }

    /// <summary>
    /// 最后登录IP
    /// </summary>
    [Column("last_login_ip")]
    [MaxLength(50)]
    public string? LastLoginIp { get; set; }

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