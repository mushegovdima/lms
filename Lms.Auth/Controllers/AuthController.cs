using Lms.Auth.Dto;
using Lms.Auth.Services.Impl;
using Lms.Auth.UserDto;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Auth.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly AuthService _authService;

    public AuthController(UserService userService, AuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] UserPostRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _userService.Register(request, cancellationToken);
        if (user is null) return BadRequest();
        return Ok(_authService.GenerateJwtToken(user));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var token = await _authService.Login(model, cancellationToken);
        return token is not null ? Ok(token) : BadRequest();
    }
}
