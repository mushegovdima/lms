using System.ComponentModel.DataAnnotations;
using Lms.SDK.Common;

namespace Lms.Auth.UserDto;

public class UserPutRequest : IPutRequest
{
    [StringLength(120)]
    public required string Name { get; set; }
}
