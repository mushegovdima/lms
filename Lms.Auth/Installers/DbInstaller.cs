using Microsoft.EntityFrameworkCore;
using Lms.Auth.Db;

namespace Lms.Auth.Installers;

public static class DbInstaller
{
    public static void InstallDb(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContextPool<DataContext>(options =>
            options.UseNpgsql(connString)
            .EnableSensitiveDataLogging()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            )
        .AddLogging();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}

