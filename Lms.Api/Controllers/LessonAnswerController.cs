using Lms.Api.Dto.LessonAnswerDto;
using Lms.Api.Dto.LessonDto;
using Lms.Api.Models;
using Lms.Api.Services;
using Lms.Api.Services.Impl;
using Lms.SDK.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class LessonAnswerController : ControllerBase
{
    private readonly ILessonAnswerService _service;

    public LessonAnswerController(ILessonAnswerService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LessonAnswerResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken = default)
    {
        var model = await _service.Get<LessonAnswerResponse>(id, cancellationToken);
        return Ok(model);
    }

    [HttpGet("getByLessonId/{id}/{authorId}")]
    [ProducesResponseType(typeof(LessonAnswerResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByLessonId(long id, long authorId, CancellationToken cancellationToken = default)
    {
        var model = await _service.GetByLessonId<LessonAnswerResponse>(id, authorId, cancellationToken);
        return Ok(model);
    }

    [HttpGet("getByFilter")]
    [ProducesResponseType(typeof(IFilterResponse<LessonListItemResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCourse([FromQuery] LessonAnswerFilter filter, CancellationToken cancellationToken = default)
    {
        var model = await _service.GetByFilter(filter, cancellationToken);
        return Ok(model);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(long id, [FromBody] LessonAnswerPutRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _service.Update(id, request, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] LessonAnswerPostRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _service.Create(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("sendToCheck/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SendToCheck(long id, CancellationToken cancellationToken = default)
    {
        await _service.SendToCheck(id, cancellationToken);
        return Ok();
    }

    [HttpPost("approve/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Approve(long id, CancellationToken cancellationToken = default)
    {
        await _service.Approve(id, cancellationToken);
        return Ok();
    }

    [HttpPost("cancel/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Cancel(long id, CancellationToken cancellationToken = default)
    {
        await _service.Cancel(id, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken = default)
    {
        await _service.Delete(id, cancellationToken);
        return Ok();
    }
}
