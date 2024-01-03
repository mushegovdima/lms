using Lms.SDK.Common;
using Lms.SDK.Enums;

namespace Lms.Api.Dto.CourseDto;

public class CourseRoleResponse: IResponse
{
    public long UserId { get; set; }
    public long CourseId { get; set; }
    public Role Role { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public long Id { get; set; }
}
