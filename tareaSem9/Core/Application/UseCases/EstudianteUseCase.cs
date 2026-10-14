using tareaSem9.Core.Domain.Entities;
using tareaSem9.Core.Ports.Inbound;
using tareaSem9.Core.Ports.Outbound;

namespace tareaSem9.Core.Application.UseCases;

public class EstudianteUseCase : IEstudianteUseCase
{
    private readonly IUnitOfWorkPort _unitOfWork;

    public EstudianteUseCase(IUnitOfWorkPort unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Estudiante>> GetAllAsync()
    {
        return await _unitOfWork.Estudiantes.GetAllAsync();
    }

    public async Task<Estudiante?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Estudiantes.GetByIdAsync(id);
    }

    public async Task<Estudiante> CreateAsync(Estudiante estudiante)
    {
        await _unitOfWork.Estudiantes.AddAsync(estudiante);
        await _unitOfWork.SaveAsync();
        return estudiante;
    }

    public async Task<bool> UpdateAsync(int id, Estudiante estudiante)
    {
        var existing = await _unitOfWork.Estudiantes.GetByIdAsync(id);
        if (existing == null)
            return false;

        existing.Nombre = estudiante.Nombre;
        existing.Edad = estudiante.Edad;
        existing.Direccion = estudiante.Direccion;
        existing.Telefono = estudiante.Telefono;
        existing.Correo = estudiante.Correo;

        await _unitOfWork.Estudiantes.UpdateAsync(existing);
        await _unitOfWork.SaveAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _unitOfWork.Estudiantes.GetByIdAsync(id);
        if (existing == null)
            return false;

        await _unitOfWork.Estudiantes.DeleteAsync(existing);
        await _unitOfWork.SaveAsync();
        return true;
    }
}
