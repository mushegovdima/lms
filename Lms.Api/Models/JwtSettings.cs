namespace Lms.Api.Models
{
    public class JwtSettings
    {
        public required string SecretKey { get; set; }
        public required int LifeTimeMinutes { get; set; } = 30;
    }
}

