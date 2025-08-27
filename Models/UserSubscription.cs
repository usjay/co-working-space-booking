using System;
using System.ComponentModel.DataAnnotations.Schema;

public class UserSubscription
{
    public int Id { get; set; }

    public string Email { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
