using tareaSem9.Core.Domain.Entities;

namespace tareaSem9.Core.Ports.Inbound;

public interface IEstudianteUseCase
{
    Task<IEnumerable<Estudiante>> GetAllAsync();
    Task<Estudiante?> GetByIdAsync(int id);
    Task<Estudiante> CreateAsync(Estudiante estudiante);
    Task<bool> UpdateAsync(int id, Estudiante estudiante);
    Task<bool> DeleteAsync(int id);
}
