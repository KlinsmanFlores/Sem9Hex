using tareaSem9.Core.Domain.Entities;

namespace tareaSem9.Core.Ports.Inbound;

public interface IProfesoreUseCase
{
    Task<IEnumerable<Profesore>> GetAllAsync();
    Task<Profesore?> GetByIdAsync(int id);
    Task<Profesore> CreateAsync(Profesore profesor);
    Task<bool> UpdateAsync(int id, Profesore profesor);
    Task<bool> DeleteAsync(int id);
}
