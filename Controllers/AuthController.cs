using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PagebaTask.DTOs;
using PagebaTask.Services;

namespace PagebaTask.Controllers
{

        [ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var user = await _authService.RegisterAsync(registerDto.Username, registerDto.Password);

        var token = await _authService.GenerateJwtTokenAsync(user);

        return Ok(new { token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var user = await _authService.LoginAsync(loginDto.Username, loginDto.Password);

        var token = await _authService.GenerateJwtTokenAsync(user);

        return Ok(new { token });
    }
}
    }
