using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreworking_space_booking_backend.Models.Facility
{
    [Table("facility")]
    public class Facility
    {
        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column("facility_id")]
        public required long FacilityId { get; set; }

        [Required]
        [Column("facility_name")]
        public required string FacilityName { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("facility_code")]
        public string FacilityCode { get; set; }

        [Column("product_id")]
        public long ProductId { get; set; }

        [Required]
        [Column("company_id")]
        public required long CompanyId { get; set; }

        [Required]
        [Column("location_id")]
        public required long LocationId { get; set; }

        [Required]
        [Column("is_default")]
        public required bool IsDefault { get; set; }

        [Required]
        [Column("is_deleted")]
        public required bool IsDeleted { get; set; }

    }
}
