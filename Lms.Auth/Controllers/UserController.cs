using Lms.Auth.Services;
using Lms.Auth.UserDto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Auth.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpPost("getByIdRange")]
    [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ByFilter([FromBody] IEnumerable<long> ids, CancellationToken cancellationToken = default)
    {
        var model = await _service.GetByIdRange<UserResponse>(ids, cancellationToken);
        return Ok(model);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken = default)
    {
        var model = await _service.Get<UserResponse>(id, cancellationToken);
        return model is null ? BadRequest() : Ok(model);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Put(long id, [FromBody] UserPutRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _service.Update(id, request, cancellationToken);
        return Ok(await _service.Get<UserResponse>(id, cancellationToken));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(long id,CancellationToken cancellationToken = default)
    {
        await _service.Delete(id, cancellationToken);
        return Ok();
    }
}
