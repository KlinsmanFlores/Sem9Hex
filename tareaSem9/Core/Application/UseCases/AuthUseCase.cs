using tareaSem9.Core.Domain.DTOs;
using tareaSem9.Core.Domain.Entities;
using tareaSem9.Core.Ports.Inbound;
using tareaSem9.Core.Ports.Outbound;

namespace tareaSem9.Core.Application.UseCases;

public class AuthUseCase : IAuthUseCase
{
    private readonly IUnitOfWorkPort _unitOfWork;
    private readonly IPasswordHasherPort _passwordHasher;
    private readonly ITokenServicePort _tokenService;

    public AuthUseCase(
        IUnitOfWorkPort unitOfWork,
        IPasswordHasherPort passwordHasher,
        ITokenServicePort tokenService)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
    {
        var usuarios = await _unitOfWork.Usuarios.GetAllAsync();
        var usuario = usuarios.FirstOrDefault(u => u.Username == loginDto.Username);

        if (usuario == null)
            return null;

        if (!_passwordHasher.VerifyPassword(loginDto.Password, usuario.Passwordhash))
            return null;

        var token = _tokenService.GenerateToken(usuario);
        return new AuthResponseDto(token, usuario.Username, usuario.Role);
    }

    public async Task RegisterUserAsync(RegisterDto registerDto)
    {
        var passwordHash = _passwordHasher.HashPassword(registerDto.Password);
        var nuevoUsuario = new Usuario
        {
            Username = registerDto.Username,
            Passwordhash = passwordHash,
            Role = registerDto.Role
        };

        await _unitOfWork.Usuarios.AddAsync(nuevoUsuario);
        await _unitOfWork.Complete();
    }
}
