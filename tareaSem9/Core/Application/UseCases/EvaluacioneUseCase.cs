using tareaSem9.Core.Domain.Entities;
using tareaSem9.Core.Ports.Inbound;
using tareaSem9.Core.Ports.Outbound;

namespace tareaSem9.Core.Application.UseCases;

public class EvaluacioneUseCase : IEvaluacioneUseCase
{
    private readonly IUnitOfWorkPort _unitOfWork;

    public EvaluacioneUseCase(IUnitOfWorkPort unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Evaluacione>> GetAllAsync()
    {
        return await _unitOfWork.Evaluaciones.GetAllAsync();
    }

    public async Task<Evaluacione?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Evaluaciones.GetByIdAsync(id);
    }

    public async Task<Evaluacione> CreateAsync(Evaluacione evaluacion)
    {
        await _unitOfWork.Evaluaciones.AddAsync(evaluacion);
        await _unitOfWork.SaveAsync();
        return evaluacion;
    }

    public async Task<bool> UpdateAsync(int id, Evaluacione evaluacion)
    {
        var existing = await _unitOfWork.Evaluaciones.GetByIdAsync(id);
        if (existing == null)
            return false;

        existing.IdEstudiante = evaluacion.IdEstudiante;
        existing.IdCurso = evaluacion.IdCurso;
        existing.Calificacion = evaluacion.Calificacion;
        existing.Fecha = evaluacion.Fecha;

        await _unitOfWork.Evaluaciones.UpdateAsync(existing);
        await _unitOfWork.SaveAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _unitOfWork.Evaluaciones.GetByIdAsync(id);
        if (existing == null)
            return false;

        await _unitOfWork.Evaluaciones.DeleteAsync(existing);
        await _unitOfWork.SaveAsync();
        return true;
    }
}
