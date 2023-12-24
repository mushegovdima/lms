namespace Lms.Auth.Models
{
    public class JwtSettings
    {
        public required string SecretKey { get; set; }
        public required int LifeTimeMinutes { get; set; } = 30;
        public required int LifeTimeRefreshTokenDays { get; set; } = 7;
    }
}

