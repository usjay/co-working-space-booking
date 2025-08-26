using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreworking_space_booking_backend.Models
{
    [Table("advertising")]
    public class Advertising
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("company_name")]
        public string CompanyName { get; set; }

        [Column("image_path")]
        public string ImagePath { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } = null;
    }
}
