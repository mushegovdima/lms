using Lms.Api.Dto.CourseDto;
using Lms.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CourseController : ControllerBase
{
    private readonly ICourseService _service;

    public CourseController(ICourseService service)
    {
        _service = service;
    }

    [HttpPost("getByIdRange")]
    [ProducesResponseType(typeof(IEnumerable<CourseResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ByFilter([FromBody] IEnumerable<long> ids, CancellationToken cancellationToken = default)
    {
        var model = await _service.GetByIdRange<CourseResponse>(ids, cancellationToken);
        return Ok(model);
    }

    [HttpGet("getByAuthor/{id}")]
    [ProducesResponseType(typeof(IEnumerable<CourseResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByAuthor(long id, CancellationToken cancellationToken = default)
    {
        var model = await _service.GetByAuthor<CourseResponse>(id, cancellationToken);
        return Ok(model);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken = default)
    {
        var model = await _service.Get<CourseResponse>(id, cancellationToken);
        return model is null ? BadRequest() : Ok(model);
    }

    [HttpPost]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] CoursePostRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _service.Create(request, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CoursePutRequest), StatusCodes.Status200OK)]
    public async Task<IActionResult> Put(long id, [FromBody] CoursePutRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _service.Update(id, request, cancellationToken);
        return Ok(await _service.Get<CourseResponse>(id, cancellationToken));
    }

}

