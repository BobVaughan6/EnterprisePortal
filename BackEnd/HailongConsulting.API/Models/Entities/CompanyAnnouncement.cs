using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HailongConsulting.API.Models.Entities
{
    [Table("company_announcements")]
    public class CompanyAnnouncement
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("title")]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Column("content")]
        public string Content { get; set; } = string.Empty;

        [Column("publish_time")]
        public DateTime? PublishTime { get; set; }

        [Column("view_count")]
        public int ViewCount { get; set; } = 0;

        [Column("attachment_path")]
        [MaxLength(500)]
        public string? AttachmentPath { get; set; }

        [Column("is_top")]
        public bool IsTop { get; set; } = false;

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}