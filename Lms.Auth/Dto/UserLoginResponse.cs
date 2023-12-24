namespace Lms.Auth.Dto
{
    public record UserTokenResponse(
        long UserId,
        string Token,
        string RefreshToken,
        DateTime ExpireDate
    );
}
