namespace Lms.SDK.Models;

public record UserModel(
    long Id,
    string Login,
    string Email,
    bool IsAdmin
);
