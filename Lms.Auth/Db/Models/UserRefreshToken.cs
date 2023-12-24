using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.Auth.Db.Models;

public class UserRefreshToken : Entity, IEntityTypeConfiguration<UserRefreshToken>
{
    public required long UserId { get; set; }
    public virtual User? User { get; set; }
    public required string Token { get; set; }
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime? Revoked { get; set; }

    [NotMapped]
    public bool IsExpired => DateTime.UtcNow >= Expires;
    
    [NotMapped]
    public bool IsRevoked => Revoked != null;
    
    [NotMapped]
    public bool IsActive => !IsRevoked && !IsExpired;

    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<UserRefreshToken>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
            
    }
}
