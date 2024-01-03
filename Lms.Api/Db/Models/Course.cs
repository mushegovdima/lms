using System.ComponentModel.DataAnnotations;
using Lms.SDK.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.Api.Db.Models;

public class Course : Entity, ICourse<Lesson, Course, LessonField>, IEntityTypeConfiguration<Course>
{
    [StringLength(250)]
    public required string Title { get; set; }

    public string Description { get; set; } = string.Empty;

    public required long AuthorId { get; set; }

    public required ICollection<Lesson> Lessons { get; set; }

    public ICollection<CourseRole> AccessRoles { get; set; } = Array.Empty<CourseRole>();

    public void Configure(EntityTypeBuilder<Course> builder)
    {
        
        builder.HasMany(x => x.AccessRoles)
            .WithOne(x => x.Course)
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
