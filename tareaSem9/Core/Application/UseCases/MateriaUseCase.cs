using tareaSem9.Core.Domain.Entities;
using tareaSem9.Core.Ports.Inbound;
using tareaSem9.Core.Ports.Outbound;

namespace tareaSem9.Core.Application.UseCases;

public class MateriaUseCase : IMateriaUseCase
{
    private readonly IUnitOfWorkPort _unitOfWork;

    public MateriaUseCase(IUnitOfWorkPort unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Materia>> GetAllAsync()
    {
        return await _unitOfWork.Materias.GetAllAsync();
    }

    public async Task<Materia?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Materias.GetByIdAsync(id);
    }

    public async Task<Materia> CreateAsync(Materia materia)
    {
        await _unitOfWork.Materias.AddAsync(materia);
        await _unitOfWork.SaveAsync();
        return materia;
    }

    public async Task<bool> UpdateAsync(int id, Materia materia)
    {
        var existing = await _unitOfWork.Materias.GetByIdAsync(id);
        if (existing == null)
            return false;

        existing.IdCurso = materia.IdCurso;
        existing.Nombre = materia.Nombre;
        existing.Descripcion = materia.Descripcion;

        await _unitOfWork.Materias.UpdateAsync(existing);
        await _unitOfWork.SaveAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _unitOfWork.Materias.GetByIdAsync(id);
        if (existing == null)
            return false;

        await _unitOfWork.Materias.DeleteAsync(existing);
        await _unitOfWork.SaveAsync();
        return true;
    }
}
