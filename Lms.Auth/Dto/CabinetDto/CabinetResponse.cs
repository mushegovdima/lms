using Lms.SDK.Common;

namespace Lms.Auth.Dto.CabinetDto;

public class CabinetResponse : IResponse
{
    public long Id { get; set; }
    public required string Title { get; set; }
}

