namespace coreworking_space_booking_backend.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace coreworking_space_booking_backend.Models
    {
        [Table("image_store")]
        public class ImageStore
        {
            [Key]
            [Column("image_id")]
            public int ImageId { get; set; }

            [Column("image_url")]
            [Required]
            public string ImageUrl { get; set; }

            [Column("description")]
            public string? Description { get; set; }

            [Column("image_type")]
            public string? ImageType { get; set; }

            [Column("product_id")]
            public int? ProductId { get; set; }

            [Column("location_id")]
            public int? LocationId { get; set; }

            [Column("user_id")]
            public int? UserId { get; set; }

            [Column("company_id")]
            public int? CompanyId { get; set; }

            [Column("advertising_id")]
            public int? AdvertisingId { get; set; }

            [Column("created_at")]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public DateTime CreatedAt { get; set; }

            [Column("updated_at")]
            [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
            public DateTime UpdatedAt { get; set; }

            [ForeignKey("ProductId")]
            public virtual Product Product { get; set; }
        }
    }

}
