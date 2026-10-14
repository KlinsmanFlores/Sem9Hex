using tareaSem9.Core.Domain.Entities;

namespace tareaSem9.Core.Ports.Inbound;

public interface IMatriculaUseCase
{
    Task<IEnumerable<Matricula>> GetAllAsync();
    Task<Matricula?> GetByIdAsync(int id);
    Task<Matricula> CreateAsync(Matricula matricula);
    Task<bool> UpdateAsync(int id, Matricula matricula);
    Task<bool> DeleteAsync(int id);
}
