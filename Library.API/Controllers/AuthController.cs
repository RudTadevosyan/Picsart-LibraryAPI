using Library.Application.Interfaces;
using Library.Shared.DTOs.Auth;
using Library.Shared.Validators;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var validator = new LoginValidator();
        var login = await validator.ValidateAsync(loginDto);
        if(!login.IsValid)
            return BadRequest(login.Errors.Select(e => e.ErrorMessage));
        
        var token = await _authService.Login(loginDto);
        return Ok(token);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var validator = new RegisterValidator();
        var register = await validator.ValidateAsync(registerDto);
        if (!register.IsValid)
            return BadRequest(register.Errors.Select(e => e.ErrorMessage));
        
        var token = await _authService.Register(registerDto);
        return Ok(token);
    }
}