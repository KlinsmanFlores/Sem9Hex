using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tareaSem9.Core.Domain.DTOs;
using tareaSem9.Core.Ports.Inbound;

namespace tareaSem9.Adapters.Inbound.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthUseCase _authUseCase;

    public AuthController(IAuthUseCase authUseCase)
    {
        _authUseCase = authUseCase;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        await _authUseCase.RegisterUserAsync(dto);
        return Ok(new { message = "Usuario registrado exitosamente con contraseña encriptada" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var response = await _authUseCase.LoginAsync(dto);
        if (response == null)
            return Unauthorized(new { message = "Usuario o contraseña incorrectos" });

        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public IActionResult GetAdminData()
    {
        return Ok(new { message = "Datos solo para administradores" });
    }

    [Authorize(Policy = "Administrador")]
    [HttpGet("admin-politica")]
    public IActionResult GetAdminPolicyData()
    {
        return Ok(new { message = "Datos accesibles mediante la política 'Administrador'" });
    }

    [Authorize(Roles = "User")]
    [HttpGet("user")]
    public IActionResult GetUserData()
    {
        return Ok(new { message = "Datos solo para usuarios con rol User" });
    }

    [Authorize]
    [HttpGet("perfil")]
    public IActionResult GetProfile()
    {
        var username = User.Identity?.Name ?? "Usuario autenticado";
        return Ok(new { message = "Acceso verificado con token JWT", usuario = username });
    }
}
