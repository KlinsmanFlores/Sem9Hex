namespace tareaSem9.Core.Domain.DTOs;

public record LoginDto(string Username, string Password);

public record RegisterDto(string Username, string Password, string Role);

public record AuthResponseDto(string Token, string Username, string Role);
