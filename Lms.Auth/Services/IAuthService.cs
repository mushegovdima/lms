using Lms.Auth.Db.Models;
using Lms.Auth.Dto;

namespace Lms.Auth.Services;

/// <summary>
/// Auth service
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Login
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<UserTokenResponse?> Login(LoginModel model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Logout
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task Logout(long userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Refresh token
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="refreshToken"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<UserTokenResponse> RefreshToken(long userId, string refreshToken, CancellationToken cancellationToken = default);

    /// <summary>
    /// Generate jwt token (with replace refresh token)
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<UserTokenResponse> GenerateJwtToken(User user, CancellationToken cancellationToken = default);
}
