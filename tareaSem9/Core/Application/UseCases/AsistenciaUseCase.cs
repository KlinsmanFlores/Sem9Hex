using tareaSem9.Core.Domain.Entities;
using tareaSem9.Core.Ports.Inbound;
using tareaSem9.Core.Ports.Outbound;

namespace tareaSem9.Core.Application.UseCases;

public class AsistenciaUseCase : IAsistenciaUseCase
{
    private readonly IUnitOfWorkPort _unitOfWork;

    public AsistenciaUseCase(IUnitOfWorkPort unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Asistencia>> GetAllAsync()
    {
        return await _unitOfWork.Asistencias.GetAllAsync();
    }

    public async Task<Asistencia?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Asistencias.GetByIdAsync(id);
    }

    public async Task<Asistencia> CreateAsync(Asistencia asistencia)
    {
        await _unitOfWork.Asistencias.AddAsync(asistencia);
        await _unitOfWork.SaveAsync();
        return asistencia;
    }

    public async Task<bool> UpdateAsync(int id, Asistencia asistencia)
    {
        var existing = await _unitOfWork.Asistencias.GetByIdAsync(id);
        if (existing == null)
            return false;

        existing.IdEstudiante = asistencia.IdEstudiante;
        existing.IdCurso = asistencia.IdCurso;
        existing.Fecha = asistencia.Fecha;
        existing.Estado = asistencia.Estado;

        await _unitOfWork.Asistencias.UpdateAsync(existing);
        await _unitOfWork.SaveAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _unitOfWork.Asistencias.GetByIdAsync(id);
        if (existing == null)
            return false;

        await _unitOfWork.Asistencias.DeleteAsync(existing);
        await _unitOfWork.SaveAsync();
        return true;
    }
}
