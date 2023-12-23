using Microsoft.EntityFrameworkCore;
using Lms.Api.Db;

namespace Lms.Api.Installers;

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

