using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.DTOs.Requests;
using Warehouse.Api.DTOs.Responses;
using Warehouse.Api.Services.Auth;

namespace Warehouse.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request)
    {
        var created = await _authService.RegisterAsync(request);
        if (created is null)
            return Conflict("Email is already registered.");

        return StatusCode(StatusCodes.Status201Created, created);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);
        if (result is null)
            return Unauthorized();

        return Ok(result);
    }
}