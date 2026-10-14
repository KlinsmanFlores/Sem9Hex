using tareaSem9.Core.Domain.DTOs;

namespace tareaSem9.Core.Ports.Inbound;

public interface IAuthUseCase
{
    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
    Task RegisterUserAsync(RegisterDto registerDto);
}
