using System.Dynamic;
using Lms.SDK.Common;

namespace Lms.Api.Dto.LessonAnswerDto;

public class LessonAnswerPostRequest : IPostRequest
{
    public long LessonId { get; set; }
    public long? CheckerId { get; set; }
    public required ExpandoObject Data { get; set; }
}
