namespace Lms.Api.Dto.Response;

public class UserResponse : IResponse
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

