using Lms.Api.Db.Models;
using Lms.Api.Dto.PostRequest;
using Lms.Api.Dto.PutRequests;
using Lms.Api.Dto.Response;
using Lms.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CabinetController : ControllerBase
{
    private readonly IEntityService<Cabinet> _service;

    public CabinetController(IEntityService<Cabinet> service)
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

