using Lms.SDK.Common;
using Lms.SDK.Enums;

namespace Lms.Auth.Dto.CabinetRoleDto;

public class CabinetRolePostRequest: IPostRequest
{
    public required long UserId { get; set; }
    public required long CabinetId { get; set; }
    public required Role Role { get; set; }
}
