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
public class CourseRoleController : ControllerBase
{
    private readonly ICourseRoleService _service;

    public CourseRoleController(ICourseRoleService service)
    {
        _service = service;
    }

    [HttpGet("getByCourse/{courseId}")]
    [ProducesResponseType(typeof(IEnumerable<CourseRoleResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCourse(long courseId, CancellationToken cancellationToken = default)
    {
        var model = await _service.GetByCourse<CourseRoleResponse>(courseId, cancellationToken);
        return Ok(model);
    }

    [HttpPost]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] CourseRolePostRequest request, CancellationToken cancellationToken = default)
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
