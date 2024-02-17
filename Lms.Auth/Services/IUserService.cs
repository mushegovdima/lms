using Lms.Auth.Db.Models;
using Lms.Auth.UserDto;
using Lms.SDK.Services;

namespace Lms.Auth.Services;

/// <summary>
/// User service
/// </summary>
public interface IUserService: IEntityService<User>
{
    /// <summary>
    /// Get user by login
    /// </summary>
    /// <param name="login"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<User?> GetUserByLogin(string login, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get user by email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<TResponse?> GetByEmail<TResponse>(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Flag user is exists
    /// </summary>
    /// <param name="login"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> UserExists(string login, CancellationToken cancellationToken = default);

    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<User?> Register(UserPostRequest model, CancellationToken cancellationToken = default);
}
