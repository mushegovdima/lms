using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using Lms.SDK.Enums;
using Lms.SDK.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.Api.Db.Models;

public class LessonField : Entity, ILessonField, IEntityTypeConfiguration<LessonField>
{
    public required long LessonId { get; set; }
    public string? Description { get; set; }
    public bool Required { get; set; }
    public FieldType Type { get; set; }
    public uint Position { get; set; }
    public required string Title { get; set; }
    public Lesson? Lesson { get; set; }

    [Column(TypeName = "jsonb")]
    public ExpandoObject? Data { get; set; } = new ExpandoObject();


    public void Configure(EntityTypeBuilder<LessonField> builder)
    {
        builder.HasOne(x => x.Lesson)
            .WithMany(x => x.Fields)
            .HasForeignKey(x => x.LessonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
