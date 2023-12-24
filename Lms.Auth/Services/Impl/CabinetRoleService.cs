using AutoMapper;
using Lms.Auth.Db;
using Lms.Auth.Db.Models;
using Lms.Auth.Dto.CabinetRoleDto;
using Lms.SDK.Enums;
using Microsoft.EntityFrameworkCore;

namespace Lms.Auth.Services.Impl;

public class CabinetRoleService : EntityServiceBase<CabinetRole>, ICabinetRoleService
{
    public CabinetRoleService(DataContext db, IMapper mapper) : base(db, mapper)
    {
    }

    public override IQueryable<CabinetRole> GetQuery()
    {
        return base.GetQuery().Include(x => x.Cabinet);
    }

    public Task<long> AddUserRole(long userId, long cabinetId, Role role, CancellationToken cancellationToken = default)
    {
        return Create(new CabinetRolePostRequest
            {
                UserId = userId,
                CabinetId = cabinetId,
                Role = role
            }, 
            cancellationToken);
    }

    public async Task RemoveUserRole(long userId, long cabinetId, Role role, CancellationToken cancellationToken = default)
    {
        var entity = await Db.CabinetRoles
            .Include(x => x.Cabinet)
            .FirstOrDefaultAsync(x => x.UserId == userId
                && x.CabinetId == cabinetId
                && x.Role == role, cancellationToken);
        
        if (entity is null) throw new KeyNotFoundException();

        await Delete(entity, cancellationToken);
    }

    public async Task Update(long id, Role newRole, CancellationToken cancellationToken = default)
    {
        var entity = await Load(id, true, cancellationToken);
        entity.Role = newRole;
        await Update(entity, cancellationToken);
    }

    public override Task<CabinetRole> Update(CabinetRole entity, CancellationToken cancellationToken = default)
    {
        if (entity.Cabinet?.AuthorId == entity.UserId)
            throw new ArgumentException("Can't update author rules");

        return base.Update(entity, cancellationToken);
    }

    public override Task Delete(CabinetRole entity, CancellationToken cancellationToken = default)
    {
        if (entity.Cabinet?.AuthorId == entity.UserId)
            throw new ArgumentException("Can't update author rules");

        return base.Delete(entity, cancellationToken);
    }
}
