using System.Security.Cryptography;
using AutoMapper;
using Lms.Auth.Db;
using Lms.Auth.Db.Models;
using Lms.Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Lms.Auth.Services.Impl;

public class UserRefreshTokenService : EntityServiceBase<UserRefreshToken>, IUserRefreshTokenService
{
    private readonly JwtSettings _jwtSettings;
    public UserRefreshTokenService(DataContext db, IMapper mapper, JwtSettings jwtSettings) : base(db, mapper)
    {
        _jwtSettings = jwtSettings;
    }

    public async Task<UserRefreshToken> GenerateRefreshToken(User user, CancellationToken cancellationToken = default)
    {
        // remove old token
        await DeleteTokenByUserId(user.Id, cancellationToken);

        var refreshToken = new UserRefreshToken
        {
            Id = 0,
            UserId = user.Id,
            Token = GetUniqueToken(),
            Expires = DateTime.UtcNow.AddDays(_jwtSettings.LifeTimeRefreshTokenDays),
        };

        await Db.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await Db.SaveChangesAsync(cancellationToken);
        return refreshToken;
    }

    public async Task DeleteTokenByUserId(long userId, CancellationToken cancellationToken = default)
    {
        var token = await Db.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        if (token is null) return;

        Db.Remove(token);
        await Db.SaveChangesAsync(cancellationToken);
    }

    public Task<UserRefreshToken?> GetToken(long userId, string refreshToken, CancellationToken cancellationToken = default)
    {
        return Db.RefreshTokens
            .FirstOrDefaultAsync(x => x.UserId == userId && x.Token == refreshToken, cancellationToken);
    }

    private string GetUniqueToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
