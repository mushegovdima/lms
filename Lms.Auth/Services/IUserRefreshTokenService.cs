using Lms.Auth.Db.Models;
using Lms.SDK.Services;

namespace Lms.Auth.Services;

/// <summary>
/// Refresh token service
/// </summary>
public interface IUserRefreshTokenService: IEntityService<UserRefreshToken>
{
    /// <summary>
    /// Generate token
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<UserRefreshToken> GenerateRefreshToken(User user, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Delete user token
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public  Task DeleteTokenByUserId(long userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get exists token
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="refreshToken"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<UserRefreshToken?> GetToken(long userId, string refreshToken, CancellationToken cancellationToken = default);
}
