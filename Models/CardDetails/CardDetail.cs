using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreworking_space_booking_backend.Models.CardDetails
{
    [Table("card_details")]
    public class CardDetail
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
        [Column("user_id")]
        public required long UserId { get; set; }

        [Required]
        [Column("card_token")]
        public required long CardToken { get; set; }

        [Required]
        [Column("card_holder_name")]
        public required string CardHolderName { get; set; }

        [Required]
        [Column("last_four_digits")]
        public required long LastFourDigits { get; set; }

        [Required]
        [Column("expiry_date")]
        public required DateOnly ExpiryDate { get; set; }

        [Required]
        [Column("is_deleted")]
        public required bool IsDeleted { get; set; }
    }
}
