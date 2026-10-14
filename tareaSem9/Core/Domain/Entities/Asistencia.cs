using System;

namespace tareaSem9.Core.Domain.Entities;

public partial class Asistencia
{
    public int IdAsistencia { get; set; }

    public int? IdEstudiante { get; set; }

    public int? IdCurso { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Estado { get; set; }

    public virtual Curso? IdCursoNavigation { get; set; }

    public virtual Estudiante? IdEstudianteNavigation { get; set; }
}
