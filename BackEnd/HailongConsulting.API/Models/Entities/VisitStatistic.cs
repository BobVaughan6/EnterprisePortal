using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HailongConsulting.API.Models.Entities;

/// <summary>
/// 访问统计实体
/// </summary>
[Table("visit_statistics")]
public class VisitStatistic
{
    /// <summary>
    /// 统计ID
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 访问日期
    /// </summary>
    [Column("visit_date")]
    [Required]
    public DateOnly VisitDate { get; set; }

    /// <summary>
    /// 页面URL
    /// </summary>
    [Column("page_url")]
    [MaxLength(500)]
    public string? PageUrl { get; set; }

    /// <summary>
    /// 页面标题
    /// </summary>
    [Column("page_title")]
    [MaxLength(255)]
    public string? PageTitle { get; set; }

    /// <summary>
    /// 访问者IP
    /// </summary>
    [Column("visitor_ip")]
    [MaxLength(50)]
    public string? VisitorIp { get; set; }

    /// <summary>
    /// 用户代理
    /// </summary>
    [Column("user_agent")]
    [MaxLength(500)]
    public string? UserAgent { get; set; }

    /// <summary>
    /// 来源页面
    /// </summary>
    [Column("referer")]
    [MaxLength(500)]
    public string? Referer { get; set; }

    /// <summary>
    /// 访问次数
    /// </summary>
    [Column("visit_count")]
    public uint VisitCount { get; set; } = 1;

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