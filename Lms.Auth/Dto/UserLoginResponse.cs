namespace Lms.Auth.Dto
{
    public class UserTokenResponse
    {
        public required string Token { get; set; }
        public required DateTime ExpireDate { get; set; }
    }
}

