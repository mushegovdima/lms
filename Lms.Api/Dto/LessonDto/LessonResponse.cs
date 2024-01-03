namespace Lms.Api.Dto.LessonDto;

public class LessonResponse : LessonListItemResponse
{
    public required ICollection<LessonFieldResponse> Fields { get; set; }
}
