using tareaSem9.Core.Domain.Entities;

namespace tareaSem9.Core.Ports.Inbound;

public interface ICursoUseCase
{
    Task<IEnumerable<Curso>> GetAllAsync();
    Task<Curso?> GetByIdAsync(int id);
    Task<Curso> CreateAsync(Curso curso);
    Task<bool> UpdateAsync(int id, Curso curso);
    Task<bool> DeleteAsync(int id);
}
