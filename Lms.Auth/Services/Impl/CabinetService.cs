using System.Security.Claims;
using AutoMapper;
using Lms.Auth.Db;
using Lms.Auth.Db.Models;
using Lms.Auth.Dto.CabinetRoleDto;
using Lms.Auth.Extensions;
using Lms.Auth.Services;
using Lms.SDK.Enums;

namespace Lms.Api.Services.Impl;

public class CabinetService : EntityServiceBase<Cabinet>, ICabinetService
{
    private readonly ICabinetRoleService _roleService;
    private readonly ClaimsPrincipal _user;
    public CabinetService(DataContext db, IMapper mapper, ICabinetRoleService roleService, IHttpContextAccessor accessor) : base(db, mapper)
    {
        _roleService = roleService;
        _user = accessor.HttpContext!.User;
    }

    public override async Task<long> Create(Cabinet entity, CancellationToken cancellationToken = default)
    {
        var cabinetId =  await base.Create(entity, cancellationToken);
        var currentUserId = _user.UserId();
        await AddUserRole(currentUserId, cabinetId, Role.Admin, cancellationToken); //by default
        return cabinetId;
    }

    public Task<long> AddUserRole(long userId, long cabinetId, Role role, CancellationToken cancellationToken = default)
    {
        return _roleService.AddUserRole(userId, cabinetId, role, cancellationToken);
    }

    public Task UpdateUserRole(long id, Role newRole, CancellationToken cancellationToken = default)
    {
        return _roleService.Update(id, newRole, cancellationToken);
    }

    public Task RemoveUserRole(long userId, long cabinetId, Role role, CancellationToken cancellationToken = default)
    {
        return _roleService.RemoveUserRole(userId, cabinetId, role, cancellationToken);
    }
}

