namespace tareaSem9.Core.Domain.Entities;

public partial class Matricula
{
    public int IdMatricula { get; set; }

    public int? IdEstudiante { get; set; }

    public int? IdCurso { get; set; }

    public string? Semestre { get; set; }

    public virtual Curso? IdCursoNavigation { get; set; }

    public virtual Estudiante? IdEstudianteNavigation { get; set; }
}
