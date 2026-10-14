using tareaSem9.Core.Domain.Entities;

namespace tareaSem9.Core.Ports.Outbound;

public interface ITokenServicePort
{
    string GenerateToken(Usuario usuario);
}
