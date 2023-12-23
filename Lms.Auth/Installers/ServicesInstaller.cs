using Lms.Api.Services.Impl;
using Lms.Auth.Services.Impl;

namespace Lms.Auth.Installers;

internal static class ServiceInstaller
{
    internal static void SetServices(this IServiceCollection services)
    {
        services.AddScoped<CabinetService>();
        services.AddScoped<UserService>();
        services.AddScoped<AuthService>();
    }
}
