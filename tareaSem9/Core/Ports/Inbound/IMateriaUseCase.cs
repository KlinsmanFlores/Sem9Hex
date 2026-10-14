using tareaSem9.Core.Domain.Entities;

namespace tareaSem9.Core.Ports.Inbound;

public interface IMateriaUseCase
{
    Task<IEnumerable<Materia>> GetAllAsync();
    Task<Materia?> GetByIdAsync(int id);
    Task<Materia> CreateAsync(Materia materia);
    Task<bool> UpdateAsync(int id, Materia materia);
    Task<bool> DeleteAsync(int id);
}
