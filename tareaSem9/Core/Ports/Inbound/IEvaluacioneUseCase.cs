using tareaSem9.Core.Domain.Entities;

namespace tareaSem9.Core.Ports.Inbound;

public interface IEvaluacioneUseCase
{
    Task<IEnumerable<Evaluacione>> GetAllAsync();
    Task<Evaluacione?> GetByIdAsync(int id);
    Task<Evaluacione> CreateAsync(Evaluacione evaluacion);
    Task<bool> UpdateAsync(int id, Evaluacione evaluacion);
    Task<bool> DeleteAsync(int id);
}
