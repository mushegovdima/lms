using Lms.Auth.Db.Models;
using Lms.SDK.Enums;
using Lms.SDK.Services;

namespace Lms.Auth.Services;

/// <summary>
/// Service of Cabinet roles
/// </summary>
public interface ICabinetRoleService: IEntityService<CabinetRole>
{
    public Task<long> AddUserRole(long userId, long cabinetId, Role role, CancellationToken cancellationToken = default);

    public Task Update(long id, Role newRole, CancellationToken cancellationToken = default);

    public Task RemoveUserRole(long userId, long cabinetId, Role role, CancellationToken cancellationToken = default);
}
