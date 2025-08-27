using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreworking_space_booking_backend.Models
{
    [Table("pricing")]
    public class Pricing
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        [Column("hourly_rate")]
        public decimal HourlyRate { get; set; }

        [Required]
        [Column("daily_rate")]
        public decimal DailyRate { get; set; }

        [Required]
        [Column("monthly_rate")]
        public decimal MonthlyRate { get; set; }

        [Column("yearly_rate")]
        public decimal YearlyRate { get; set; }

        [Column("location_id")]              
        [Required]
        public int LocationId { get; set; }


        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}
