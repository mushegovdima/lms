using Lms.Auth.Db.Models;
using Lms.Auth.Dto.CabinetDto;
using Lms.Auth.Services;
using Lms.SDK.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CabinetController : ControllerBase
{
    private readonly ICabinetService _service;

    public CabinetController(ICabinetService service)
    {
        _service = service;
    }

    [HttpPost("getByIdRange")]
    [ProducesResponseType(typeof(IEnumerable<CabinetResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ByFilter([FromBody] IEnumerable<long> ids, CancellationToken cancellationToken = default)
    {
        var model = await _service.GetByIdRange<CabinetResponse>(ids, cancellationToken);
        return Ok(model);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken = default)
    {
        var model = await _service.Get<CabinetResponse>(id, cancellationToken);
        return model is null ? BadRequest() : Ok(model);
    }

    [HttpPost]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] CabinetPostRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _service.Create(request, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CabinetResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Put(long id, [FromBody] CabinetPutRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _service.Update(id, request, cancellationToken);
        return Ok(await _service.Get<CabinetResponse>(id, cancellationToken));
    }

}
