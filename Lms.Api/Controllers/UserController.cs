using Lms.Api.Db.Models;
using Lms.Api.Dto.PostRequest;
using Lms.Api.Dto.Response;
using Lms.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IEntityService<User> _service;

    public UserController(IEntityService<User> service)
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

    [HttpPost]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] UserPostRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _service.Create(request, cancellationToken);
        return Ok(result);
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
