using Lms.Auth.Db.Models;
using Lms.SDK.Enums;
using Lms.SDK.Services;

namespace Lms.Auth.Services;

/// <summary>
/// Cabinet service
/// </summary>
public interface ICabinetService: IEntityService<Cabinet>
{
    /// <summary>
    /// Add user role for cabinet
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cabinetId"></param>
    /// <param name="role"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<long> AddUserRole(long userId, long cabinetId, Role role, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить роль для кабинета
    /// </summary>
    /// <param name=""></param>
    /// <param name="newRole"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task UpdateUserRole(long id, Role newRole, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove user role
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cabinetId"></param>
    /// <param name="role"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task RemoveUserRole(long userId, long cabinetId, Role role, CancellationToken cancellationToken = default);
}
