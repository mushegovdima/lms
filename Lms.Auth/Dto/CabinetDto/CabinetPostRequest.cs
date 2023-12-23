using Lms.SDK.Common;

namespace Lms.Auth.Dto.CabinetDto;

public class CabinetPostRequest : IPostRequest
{
    public required string Title { get; set; }
}
