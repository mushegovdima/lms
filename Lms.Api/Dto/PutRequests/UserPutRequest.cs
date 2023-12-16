using System;
using System.ComponentModel.DataAnnotations;
namespace Lms.Api.Dto.PostRequest;

public class UserPutRequest : IPutRequest
{
    [StringLength(120)]
    public required string Name { get; set; }
}
