using Lms.Api.Services.Impl;

namespace Lms.Api.Services;

internal static class ServiceInstallExtensions
{
    internal static void SetServices(this IServiceCollection services)
    {
        services.AddScoped<LessonService>();
        services.AddScoped<LessonAnswerService>();
        services.AddScoped<ICourseRoleService, CourseRoleService>();
        services.AddScoped<CourseService>();
    }
}

