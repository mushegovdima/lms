using Lms.Api.Services.Impl;

namespace Lms.Api.Services;

internal static class ServiceInstallExtensions
{
    internal static void SetServices(this IServiceCollection services)
    {
        services.AddScoped<ILessonService, LessonService>();
        services.AddScoped<ILessonAnswerService, LessonAnswerService>();
        services.AddScoped<ICourseRoleService, CourseRoleService>();
        services.AddScoped<ICourseService, CourseService>();
    }
}

