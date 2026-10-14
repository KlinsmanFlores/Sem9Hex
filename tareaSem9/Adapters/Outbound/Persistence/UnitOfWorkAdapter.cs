using System.Collections;
using tareaSem9.Core.Domain.Entities;
using tareaSem9.Core.Ports.Outbound;

namespace tareaSem9.Adapters.Outbound.Persistence;

public class UnitOfWorkAdapter : IUnitOfWorkPort
{
    private readonly Hashtable _repositories = new();
    private readonly ApplicationDbContext _context;

    public IGenericRepositoryPort<Asistencia> Asistencias => Repository<Asistencia>();
    public IGenericRepositoryPort<Curso> Cursos => Repository<Curso>();
    public IGenericRepositoryPort<Estudiante> Estudiantes => Repository<Estudiante>();
    public IGenericRepositoryPort<Evaluacione> Evaluaciones => Repository<Evaluacione>();
    public IGenericRepositoryPort<Materia> Materias => Repository<Materia>();
    public IGenericRepositoryPort<Matricula> Matriculas => Repository<Matricula>();
    public IGenericRepositoryPort<Profesore> Profesores => Repository<Profesore>();
    public IGenericRepositoryPort<Usuario> Usuarios => Repository<Usuario>();

    public UnitOfWorkAdapter(ApplicationDbContext context)
    {
        _context = context;
    }

    public IGenericRepositoryPort<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity).Name;

        if (_repositories.ContainsKey(type))
        {
            return (IGenericRepositoryPort<TEntity>)_repositories[type]!;
        }

        var repositoryType = typeof(GenericRepositoryAdapter<>);

        var repositoryInstance = Activator.CreateInstance(
            repositoryType.MakeGenericType(typeof(TEntity)),
            _context);

        if (repositoryInstance == null)
        {
            throw new Exception($"No se pudo crear el adaptador de repositorio para {type}");
        }

        _repositories.Add(type, repositoryInstance);

        return (IGenericRepositoryPort<TEntity>)repositoryInstance;
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public Task<int> Complete()
    {
        return SaveAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
