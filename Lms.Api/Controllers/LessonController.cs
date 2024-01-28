using Lms.Api.Dto.LessonDto;
using Lms.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class LessonController : ControllerBase
{
    private readonly ILessonService _service;

    public LessonController(ILessonService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LessonResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken = default)
    {
        var model = await _service.Get<LessonResponse>(id, cancellationToken);
        return Ok(model);
    }

    [HttpGet("getByCourse/{courseId}")]
    [ProducesResponseType(typeof(IEnumerable<LessonListItemResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCourse(long courseId, CancellationToken cancellationToken = default)
    {
        var model = await _service.GetByCourse<LessonListItemResponse>(courseId, cancellationToken);
        return Ok(model);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<IActionResult> Put(long id, [FromBody] LessonPutRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _service.Update(request, cancellationToken);
        return Ok(await _service.Get<LessonResponse>(id, cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] LessonPostRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _service.Create(request, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken = default)
    {
        await _service.Delete(id, cancellationToken);
        return Ok();
    }
}
