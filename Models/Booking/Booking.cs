using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreworking_space_booking_backend.Models.Booking
{
    [Table("booking")]
    public class Booking
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
        [Column("booking_id")]
        public required long BookingId { get; set; }

        [Required]
        [Column("user_id")]
        public required long UserId { get; set; }

        [Required]
        [Column("product_id")]
        public long ProductId { get; set; }

        [Required]
        [Column("location_id")]
        public required long LocationId { get; set; }

        [Required]
        [Column("payment_id")]
        public required long PaymentId { get; set; }

        [Column("facility_id")]
        public long FacilityId { get; set; }

        [Column("facility_code")]
        public string FacilityCode { get; set; }

        [Required]
        [Column("start_time")]
        public required DateTime StartTime { get; set; }

        [Required]
        [Column("end_time")]
        public required DateTime EndTime { get; set; }

        [Required]
        [Column("is_onetime_changed")]
        public required bool IsOnetimeChanged { get; set; }

        [Required]
        [Column("is_cansled")]
        public required bool IsCanseled { get; set; }

    }
}