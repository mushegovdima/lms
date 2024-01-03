using System.Dynamic;
using Lms.SDK.Common;

namespace Lms.Api.Dto.LessonAnswerDto;

public class LessonAnswerPutRequest : IPutRequest
{
    public required ExpandoObject Data { get; set; }
}
