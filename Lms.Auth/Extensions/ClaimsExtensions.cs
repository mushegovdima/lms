using System.Security.Claims;

namespace Lms.Auth.Extensions;

public static class ClaimsExtensions
{
    public static long UserId(this ClaimsPrincipal user)
    {
        var idStr = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!long.TryParse(idStr, out var id))
            throw new KeyNotFoundException($"UserId not found{idStr}");
        
        return id;
    }
}
