using Lms.Api.Db.Models;
using Lms.SDK.Common;
using Microsoft.EntityFrameworkCore;

namespace Lms.Api.Db;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<LessonField> LessonFields { get; set; }
    public DbSet<LessonAnswer> LessonAnswers { get; set; }


    public override int SaveChanges()
    {
        ApplySoftDelete();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplySoftDelete();
        return base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Detect softedeletable entities
    /// </summary>
    private void ApplySoftDelete()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is ISoftDeletable softDeleteEntity && entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;

                softDeleteEntity.IsDeleted = true; // Set flag IsDeleted as true
            }
        }
    }

}

