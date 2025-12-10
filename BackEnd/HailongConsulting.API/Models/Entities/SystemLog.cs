using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HailongConsulting.API.Models.Entities;

/// <summary>
/// 系统操作日志实体
/// </summary>
[Table("system_logs")]
public class SystemLog
{
    /// <summary>
    /// 日志ID
    /// </summary>
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    /// <summary>
    /// 操作用户ID
    /// </summary>
    [Column("user_id")]
    public int? UserId { get; set; }

    /// <summary>
    /// 操作用户名
    /// </summary>
    [Column("username")]
    [MaxLength(50)]
    public string? Username { get; set; }

    /// <summary>
    /// 操作动作
    /// </summary>
    [Column("action")]
    [Required]
    [MaxLength(100)]
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// 操作模块
    /// </summary>
    [Column("module")]
    [MaxLength(50)]
    public string? Module { get; set; }

    /// <summary>
    /// 操作描述
    /// </summary>
    [Column("description")]
    public string? Description { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    [Column("ip_address")]
    [MaxLength(50)]
    public string? IpAddress { get; set; }

    /// <summary>
    /// 用户代理
    /// </summary>
    [Column("user_agent")]
    [MaxLength(500)]
    public string? UserAgent { get; set; }

    /// <summary>
    /// 请求数据（JSON）
    /// </summary>
    [Column("request_data")]
    public string? RequestData { get; set; }

    /// <summary>
    /// 响应数据（JSON）
    /// </summary>
    [Column("response_data")]
    public string? ResponseData { get; set; }

    /// <summary>
    /// 操作状态：success、error
    /// </summary>
    [Column("status")]
    [MaxLength(20)]
    public string Status { get; set; } = "success";

    /// <summary>
    /// 创建时间
    /// </summary>
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}