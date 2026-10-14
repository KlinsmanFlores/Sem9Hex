using tareaSem9.Core.Domain.Entities;
using tareaSem9.Core.Ports.Inbound;
using tareaSem9.Core.Ports.Outbound;

namespace tareaSem9.Core.Application.UseCases;

public class ProfesoreUseCase : IProfesoreUseCase
{
    private readonly IUnitOfWorkPort _unitOfWork;

    public ProfesoreUseCase(IUnitOfWorkPort unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Profesore>> GetAllAsync()
    {
        return await _unitOfWork.Profesores.GetAllAsync();
    }

    public async Task<Profesore?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Profesores.GetByIdAsync(id);
    }

    public async Task<Profesore> CreateAsync(Profesore profesor)
    {
        await _unitOfWork.Profesores.AddAsync(profesor);
        await _unitOfWork.SaveAsync();
        return profesor;
    }

    public async Task<bool> UpdateAsync(int id, Profesore profesor)
    {
        var existing = await _unitOfWork.Profesores.GetByIdAsync(id);
        if (existing == null)
            return false;

        existing.Nombre = profesor.Nombre;
        existing.Especialidad = profesor.Especialidad;
        existing.Correo = profesor.Correo;

        await _unitOfWork.Profesores.UpdateAsync(existing);
        await _unitOfWork.SaveAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _unitOfWork.Profesores.GetByIdAsync(id);
        if (existing == null)
            return false;

        await _unitOfWork.Profesores.DeleteAsync(existing);
        await _unitOfWork.SaveAsync();
        return true;
    }
}
