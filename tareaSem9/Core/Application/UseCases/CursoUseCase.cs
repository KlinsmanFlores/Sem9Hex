using tareaSem9.Core.Domain.Entities;
using tareaSem9.Core.Ports.Inbound;
using tareaSem9.Core.Ports.Outbound;

namespace tareaSem9.Core.Application.UseCases;

public class CursoUseCase : ICursoUseCase
{
    private readonly IUnitOfWorkPort _unitOfWork;

    public CursoUseCase(IUnitOfWorkPort unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Curso>> GetAllAsync()
    {
        return await _unitOfWork.Cursos.GetAllAsync();
    }

    public async Task<Curso?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Cursos.GetByIdAsync(id);
    }

    public async Task<Curso> CreateAsync(Curso curso)
    {
        await _unitOfWork.Cursos.AddAsync(curso);
        await _unitOfWork.SaveAsync();
        return curso;
    }

    public async Task<bool> UpdateAsync(int id, Curso curso)
    {
        var existing = await _unitOfWork.Cursos.GetByIdAsync(id);
        if (existing == null)
            return false;

        existing.Nombre = curso.Nombre;
        existing.Descripcion = curso.Descripcion;
        existing.Creditos = curso.Creditos;

        await _unitOfWork.Cursos.UpdateAsync(existing);
        await _unitOfWork.SaveAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _unitOfWork.Cursos.GetByIdAsync(id);
        if (existing == null)
            return false;

        await _unitOfWork.Cursos.DeleteAsync(existing);
        await _unitOfWork.SaveAsync();
        return true;
    }
}
