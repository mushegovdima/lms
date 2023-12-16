using Lms.Api.Db.Models;
using Lms.Api.Services.Impl;

namespace Lms.Api.Services;

internal static class ServiceInstallExtensions
{
    internal static void SetServices(this IServiceCollection services)
    {
        services.AddScoped<IEntityService<Cabinet>, CabinetService>();
        services.AddScoped<IEntityService<User>, UserService>();
        services.AddScoped<IEntityService<Lesson>, LessonService>();
        services.AddScoped<IEntityService<Course>, CourseService>();
    }
}

