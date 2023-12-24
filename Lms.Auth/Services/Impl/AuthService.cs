using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Lms.Auth.Dto;
using Lms.Auth.Db.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Lms.Auth.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Lms.SDK.Models;

namespace Lms.Auth.Services.Impl
{
    public class AuthService: IAuthService
    {
        private readonly IUserService _userService;
        private readonly JwtSettings _jwtSettings;
        private readonly IUserRefreshTokenService _tokenService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserService userService, JwtSettings jwtSettings, IUserRefreshTokenService tokenService, ILogger<AuthService> logger)
        {
            _userService = userService;
            _jwtSettings = jwtSettings;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<UserTokenResponse?> Login(LoginModel model, CancellationToken cancellationToken = default)
        {
            var user = await _userService.GetUserByLogin(model.Login, cancellationToken);
            if (user is null || !VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            _logger.LogDebug($"Login: User {user.Id}");
            return await GenerateJwtToken(user, cancellationToken);
        }

        public async Task Logout(long userId, CancellationToken cancellationToken = default)
        {
            await _tokenService.DeleteTokenByUserId(userId, cancellationToken);
            _logger.LogDebug($"Logout: User {userId}");
        }

        public async Task<UserTokenResponse> RefreshToken(long userId, string refreshToken, CancellationToken cancellationToken = default)
        {
            var token = await _tokenService.GetToken(userId, refreshToken, cancellationToken);
            if (token is null) throw new InvalidCastException($"Token is not found");

            if (token.IsExpired)
            {
                await _tokenService.Delete(token.Id, cancellationToken);
                throw new InvalidCastException($"Token is expired");
            }
            if (token.IsRevoked)
            {
                await _tokenService.Delete(token.Id, cancellationToken);
                throw new InvalidCastException($"Token is revoked");
            }
            var user = await _userService.Load(userId, false, cancellationToken);

            _logger.LogDebug($"Refresh token: User {user.Id}");
            return await GenerateJwtToken(user, cancellationToken);
        }

        public async Task<UserTokenResponse> GenerateJwtToken(User user, CancellationToken cancellationToken = default)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
            var expireDate = DateTime.UtcNow.AddMinutes(_jwtSettings.LifeTimeMinutes);
            var identity = GetIdentity(user);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = expireDate, // expire time of token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = await _tokenService.GenerateRefreshToken(user, cancellationToken);
            return new UserTokenResponse(
                user.Id,
                tokenHandler.WriteToken(token),
                refreshToken.Token,
                expireDate
            );
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            var access = JsonConvert.SerializeObject(user.CabinetAccess.Select(x => new CabinetAccessModel(x.CabinetId, x.Role)));
            return new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Access", access)
                });
            ;
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != storedHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}

