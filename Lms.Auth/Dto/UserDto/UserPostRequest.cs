using System.ComponentModel.DataAnnotations;
using Lms.SDK.Common;

namespace Lms.Auth.UserDto;

public class UserPostRequest : IPostRequest
{
    [StringLength(120)]
    public required string Login { get; set; }

    [StringLength(120)]
    public required string Password { get; set; }

    [StringLength(120)]
    public required string Name { get; set; }

    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [DataType(DataType.PhoneNumber)]
    public required string Phone { get; set; }
}
