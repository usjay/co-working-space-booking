using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreworking_space_booking_backend.Models.Permission
{
    [Table("permission")]
    public class Permission
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
        [Column("permission_id")]
        public required long PermissionId { get; set; }

        [Required]
        [Column("description")]
        public required string Description { get; set; }

        [Column("role_id")]
        public long RoleId { get; set; }

        [Required]
        [Column("is_deleted")]
        public required bool IsDeleted { get; set; }
    }
}
