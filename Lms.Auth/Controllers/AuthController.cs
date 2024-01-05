using Lms.Auth.Dto;
using Lms.SDK.Extensions;
using Lms.Auth.Services;
using Lms.Auth.Services.Impl;
using Lms.Auth.UserDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Lms.Auth.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AuthController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] UserPostRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _userService.Register(request, cancellationToken);
        if (user is null) return BadRequest();

        return Ok(await _authService.GenerateJwtToken(user, cancellationToken));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserTokenResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginModel model, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var token = await _authService.Login(model, cancellationToken);
        return token is not null ? Ok(token) : BadRequest();
    }

    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken = default)
    {
        var userId = User.UserId();
        await _authService.Logout(userId, cancellationToken);
        return Ok();
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(UserTokenResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var userId = User.UserId();
        try
        {
            var token = await _authService.RefreshToken(userId, refreshToken, cancellationToken);
            return Ok(token);
        } catch(Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
