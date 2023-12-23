using System;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Lms.Auth.Dto;
using Lms.Auth.Db.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Lms.Auth.Models;

namespace Lms.Auth.Services.Impl
{
    public class AuthService
    {
        private readonly UserService _userService;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserService userService, JwtSettings jwtSettings)
        {
            _userService = userService;
            _jwtSettings = jwtSettings;
        }

        [HttpPost("login")]
        public async Task<UserTokenResponse?> Login(LoginModel model, CancellationToken cancellationToken = default)
        {
            var user = await _userService.GetUserByLogin(model.Login, cancellationToken);
            if (user is null || !VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            var token = GenerateJwtToken(user);
            return token;
        }

        internal UserTokenResponse GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
            var expireDate = DateTime.UtcNow.AddMinutes(_jwtSettings.LifeTimeMinutes);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Email, user.Email),
                }),
                Expires = expireDate, // expire time of token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new UserTokenResponse { Token = tokenHandler.WriteToken(token), ExpireDate = expireDate };
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

