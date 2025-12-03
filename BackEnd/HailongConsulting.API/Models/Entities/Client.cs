using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HailongConsulting.API.Models.Entities;

/// <summary>
/// 客户实体
/// </summary>
[Table("clients")]
public class Client
{
    [Key]
    [Column("client_id")]
    public int ClientId { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("client_name")]
    public string ClientName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [Column("client_code")]
    public string ClientCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [Column("industry")]
    public string Industry { get; set; } = string.Empty;

    [MaxLength(100)]
    [Column("contact_person")]
    public string? ContactPerson { get; set; }

    [MaxLength(100)]
    [Column("contact_email")]
    public string? ContactEmail { get; set; }

    [MaxLength(20)]
    [Column("contact_phone")]
    public string? ContactPhone { get; set; }

    [MaxLength(200)]
    [Column("address")]
    public string? Address { get; set; }

    [MaxLength(500)]
    [Column("website")]
    public string? Website { get; set; }

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [MaxLength(500)]
    [Column("logo_url")]
    public string? LogoUrl { get; set; }

    [Required]
    [MaxLength(20)]
    [Column("status")]
    public string Status { get; set; } = "active";

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}