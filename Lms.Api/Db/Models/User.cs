using System.ComponentModel.DataAnnotations;
using Lms.SDK.Interfaces;

namespace Lms.Api.Db.Models;

/// <summary>
/// TODO: relocate to separate AuthService
/// </summary>
public class User : Entity, IUser
{
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [DataType(DataType.PhoneNumber)]
    public required string Phone { get; set; }

    [StringLength(120)]
    public required string Name { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
}
