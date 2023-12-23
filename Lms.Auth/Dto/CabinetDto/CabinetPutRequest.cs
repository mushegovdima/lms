using Lms.SDK.Common;

namespace Lms.Auth.Dto.CabinetDto;

public class CabinetPutRequest : IPutRequest
{
    public required string Title { get; set; }
}

