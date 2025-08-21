using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreworking_space_booking_backend.Models
{
    [Table("product")]
    public class Product
    {
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("company_id")]
        public int CompanyId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("location_id")]
        public int LocationId { get; set; }

        [Column("operation_hours")]
        public string OperationHours { get; set; }

        [Column("capacity")]
        public int Capacity { get; set; }

        [Column("image")]
        public string Image { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("features")]
        public string Features { get; set; }

        [Column("images")]
        public string Images { get; set; }

        [Column("availability")]
        public string Availability { get; set; }

        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}
