using System.ComponentModel.DataAnnotations;
using Lms.SDK.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.Auth.Db.Models;

public class User : Entity, IUser, IEntityTypeConfiguration<User>
{
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [DataType(DataType.PhoneNumber)]
    public required string Phone { get; set; }

    [StringLength(120)]
    public required string Name { get; set; }

    [StringLength(120)]
    public required string Login { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public required byte[] PasswordHash { get; set; }

    public required byte[] PasswordSalt { get; set; }

    public ICollection<CabinetRole> CabinetAccess { get; set; } = Array.Empty<CabinetRole>();

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => x.Login).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasMany(x => x.CabinetAccess)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
