using tareaSem9.Core.Domain.Entities;

namespace tareaSem9.Core.Ports.Outbound;

public interface IUnitOfWorkPort : IDisposable
{
    IGenericRepositoryPort<Asistencia> Asistencias { get; }
    IGenericRepositoryPort<Curso> Cursos { get; }
    IGenericRepositoryPort<Estudiante> Estudiantes { get; }
    IGenericRepositoryPort<Evaluacione> Evaluaciones { get; }
    IGenericRepositoryPort<Materia> Materias { get; }
    IGenericRepositoryPort<Matricula> Matriculas { get; }
    IGenericRepositoryPort<Profesore> Profesores { get; }
    IGenericRepositoryPort<Usuario> Usuarios { get; }

    IGenericRepositoryPort<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> SaveAsync();
    Task<int> Complete();
}
