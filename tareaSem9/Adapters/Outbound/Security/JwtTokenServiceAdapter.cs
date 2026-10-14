using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using tareaSem9.Core.Domain.Entities;
using tareaSem9.Core.Ports.Outbound;

namespace tareaSem9.Adapters.Outbound.Security;

public class JwtTokenServiceAdapter : ITokenServicePort
{
    private readonly IConfiguration _configuration;

    public JwtTokenServiceAdapter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario.Username),
            new Claim(ClaimTypes.Role, usuario.Role)
        };

        var secretKey = _configuration["Jwt:SecretKey"] ?? "SuperSecretKeyForJwtTokenAuth123456!";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"] ?? "tareaSem9",
            audience: _configuration["Jwt:Audience"] ?? "tareaSem9",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
