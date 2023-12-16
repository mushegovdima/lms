using System;
using System.ComponentModel.DataAnnotations;
namespace Lms.Api.Dto.PostRequest;

public class UserPostRequest : IPostRequest
{
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [DataType(DataType.PhoneNumber)]
    public required string Phone { get; set; }

    [StringLength(120)]
    public required string Name { get; set; }
}
