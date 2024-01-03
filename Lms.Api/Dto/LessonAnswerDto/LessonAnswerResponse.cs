using System.Dynamic;

namespace Lms.Api.Dto.LessonAnswerDto;

public class LessonAnswerResponse : LessonAnswerListItemResponse
{
    public required ExpandoObject Data { get; set; }
}
