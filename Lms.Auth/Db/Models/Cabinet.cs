using Lms.SDK.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.Auth.Db.Models;

/// <summary>
/// Cabinet 
/// </summary>
public class Cabinet : Entity, ICabinet, IEntityTypeConfiguration<Cabinet>
{
    public required string Title { get; set; }
    public required long AuthorId { get; set; }
    public required DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public ICollection<CabinetRole> AccessRoles { get; set; } = Array.Empty<CabinetRole>();

    public void Configure(EntityTypeBuilder<Cabinet> builder)
    {
        builder.HasMany(x => x.AccessRoles)
            .WithOne(x => x.Cabinet)
            .HasForeignKey(x => x.CabinetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
