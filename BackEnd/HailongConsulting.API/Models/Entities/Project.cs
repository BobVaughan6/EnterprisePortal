using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HailongConsulting.API.Models.Entities;

/// <summary>
/// 项目实体
/// </summary>
[Table("projects")]
public class Project
{
    [Key]
    [Column("project_id")]
    public int ProjectId { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("project_name")]
    public string ProjectName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [Column("project_code")]
    public string ProjectCode { get; set; } = string.Empty;

    [Column("client_id")]
    public int? ClientId { get; set; }

    [Column("category_id")]
    public int? CategoryId { get; set; }

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("start_date")]
    public DateTime? StartDate { get; set; }

    [Column("end_date")]
    public DateTime? EndDate { get; set; }

    [Required]
    [MaxLength(20)]
    [Column("status")]
    public string Status { get; set; } = "planning";

    [Column("budget", TypeName = "decimal(15,2)")]
    public decimal? Budget { get; set; }

    [Column("actual_cost", TypeName = "decimal(15,2)")]
    public decimal? ActualCost { get; set; }

    [Column("project_manager_id")]
    public int? ProjectManagerId { get; set; }

    [MaxLength(500)]
    [Column("cover_image")]
    public string? CoverImage { get; set; }

    [Column("tags", TypeName = "json")]
    public string? Tags { get; set; }

    [Column("is_featured")]
    public bool IsFeatured { get; set; } = false;

    [Column("view_count")]
    public int ViewCount { get; set; } = 0;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    [ForeignKey("ClientId")]
    public virtual Client? Client { get; set; }

    [ForeignKey("CategoryId")]
    public virtual Category? Category { get; set; }

    [ForeignKey("ProjectManagerId")]
    public virtual User? ProjectManager { get; set; }
}