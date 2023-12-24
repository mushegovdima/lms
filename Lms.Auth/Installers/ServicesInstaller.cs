using Lms.Api.Services.Impl;
using Lms.Auth.Services;
using Lms.Auth.Services.Impl;

namespace Lms.Auth.Installers;

internal static class ServiceInstaller
{
    internal static void SetServices(this IServiceCollection services)
    {
        services.AddScoped<ICabinetService, CabinetService>();
        services.AddScoped<ICabinetRoleService, CabinetRoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserRefreshTokenService, UserRefreshTokenService>();
    }
}
