using Lms.SDK.Common;
using Lms.SDK.Enums;

namespace Lms.Api.Dto.CourseDto;

public class CourseRolePostRequest: IPostRequest
{
    public required long UserId { get; set; }
    public required long CourseId { get; set; }
    public required Role Role { get; set; }
}
