using System.Security.Claims;
using System.Text.Json;
using Lms.SDK.Models;

namespace Lms.SDK.Extensions;

public static class ClaimsExtensions
{
    public static long UserId(this ClaimsPrincipal user)
    {
        var idStr = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!long.TryParse(idStr, out var id))
            throw new KeyNotFoundException($"UserId not found{idStr}");
        
        return id;
    }

    public static bool IsAdmin(this ClaimsPrincipal user)
    {
        var str = user.FindFirstValue(ClaimTypes.NameIdentifier);
        return !bool.TryParse(str, out var result) || result;
    }

    public static UserModel GetUserModel(this ClaimsPrincipal user)
    {
        var userId = user.UserId();
        var isAdmin = user.IsAdmin();
        var login = user.FindFirstValue(ClaimTypes.Name) ?? throw new KeyNotFoundException($"login not found");
        var email = user.FindFirstValue(ClaimTypes.Email) ?? throw new KeyNotFoundException($"email not found");
        return new UserModel(userId, login, email, isAdmin);
    }
}
