using tareaSem9.Core.Domain.Entities;

namespace tareaSem9.Core.Ports.Inbound;

public interface IAsistenciaUseCase
{
    Task<IEnumerable<Asistencia>> GetAllAsync();
    Task<Asistencia?> GetByIdAsync(int id);
    Task<Asistencia> CreateAsync(Asistencia asistencia);
    Task<bool> UpdateAsync(int id, Asistencia asistencia);
    Task<bool> DeleteAsync(int id);
}
