using tareaSem9.Core.Domain.Entities;
using tareaSem9.Core.Ports.Inbound;
using tareaSem9.Core.Ports.Outbound;

namespace tareaSem9.Core.Application.UseCases;

public class MatriculaUseCase : IMatriculaUseCase
{
    private readonly IUnitOfWorkPort _unitOfWork;

    public MatriculaUseCase(IUnitOfWorkPort unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Matricula>> GetAllAsync()
    {
        return await _unitOfWork.Matriculas.GetAllAsync();
    }

    public async Task<Matricula?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Matriculas.GetByIdAsync(id);
    }

    public async Task<Matricula> CreateAsync(Matricula matricula)
    {
        await _unitOfWork.Matriculas.AddAsync(matricula);
        await _unitOfWork.SaveAsync();
        return matricula;
    }

    public async Task<bool> UpdateAsync(int id, Matricula matricula)
    {
        var existing = await _unitOfWork.Matriculas.GetByIdAsync(id);
        if (existing == null)
            return false;

        existing.IdEstudiante = matricula.IdEstudiante;
        existing.IdCurso = matricula.IdCurso;
        existing.Semestre = matricula.Semestre;

        await _unitOfWork.Matriculas.UpdateAsync(existing);
        await _unitOfWork.SaveAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _unitOfWork.Matriculas.GetByIdAsync(id);
        if (existing == null)
            return false;

        await _unitOfWork.Matriculas.DeleteAsync(existing);
        await _unitOfWork.SaveAsync();
        return true;
    }
}
