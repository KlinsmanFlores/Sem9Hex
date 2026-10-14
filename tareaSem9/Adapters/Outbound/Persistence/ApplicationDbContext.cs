using System;
using Microsoft.EntityFrameworkCore;
using tareaSem9.Core.Domain.Entities;

namespace tareaSem9.Adapters.Outbound.Persistence;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencia> Asistencias { get; set; }
    public virtual DbSet<Curso> Cursos { get; set; }
    public virtual DbSet<Estudiante> Estudiantes { get; set; }
    public virtual DbSet<Evaluacione> Evaluaciones { get; set; }
    public virtual DbSet<Materia> Materias { get; set; }
    public virtual DbSet<Matricula> Matriculas { get; set; }
    public virtual DbSet<Profesore> Profesores { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencia>(entity =>
        {
            entity.HasKey(e => e.IdAsistencia).HasName("asistencias_pkey");
            entity.ToTable("asistencias");

            entity.Property(e => e.IdAsistencia).HasColumnName("id_asistencia");
            entity.Property(e => e.Estado).HasMaxLength(20).HasColumnName("estado");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdCurso).HasColumnName("id_curso");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdCurso)
                .HasConstraintName("asistencias_id_curso_fkey");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdEstudiante)
                .HasConstraintName("asistencias_id_estudiante_fkey");
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("cursos_pkey");
            entity.ToTable("cursos");

            entity.Property(e => e.IdCurso).HasColumnName("id_curso");
            entity.Property(e => e.Creditos).HasColumnName("creditos");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Nombre).HasMaxLength(100).HasColumnName("nombre");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante).HasName("estudiantes_pkey");
            entity.ToTable("estudiantes");

            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.Correo).HasMaxLength(100).HasColumnName("correo");
            entity.Property(e => e.Direccion).HasMaxLength(255).HasColumnName("direccion");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Nombre).HasMaxLength(100).HasColumnName("nombre");
            entity.Property(e => e.Telefono).HasMaxLength(20).HasColumnName("telefono");
        });

        modelBuilder.Entity<Evaluacione>(entity =>
        {
            entity.HasKey(e => e.IdEvaluacion).HasName("evaluaciones_pkey");
            entity.ToTable("evaluaciones");

            entity.Property(e => e.IdEvaluacion).HasColumnName("id_evaluacion");
            entity.Property(e => e.Calificacion).HasPrecision(5, 2).HasColumnName("calificacion");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdCurso).HasColumnName("id_curso");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Evaluaciones)
                .HasForeignKey(d => d.IdCurso)
                .HasConstraintName("evaluaciones_id_curso_fkey");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Evaluaciones)
                .HasForeignKey(d => d.IdEstudiante)
                .HasConstraintName("evaluaciones_id_estudiante_fkey");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.IdMateria).HasName("materias_pkey");
            entity.ToTable("materias");

            entity.Property(e => e.IdMateria).HasColumnName("id_materia");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.IdCurso).HasColumnName("id_curso");
            entity.Property(e => e.Nombre).HasMaxLength(100).HasColumnName("nombre");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Materia)
                .HasForeignKey(d => d.IdCurso)
                .HasConstraintName("materias_id_curso_fkey");
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.IdMatricula).HasName("matriculas_pkey");
            entity.ToTable("matriculas");

            entity.Property(e => e.IdMatricula).HasColumnName("id_matricula");
            entity.Property(e => e.IdCurso).HasColumnName("id_curso");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.Semestre).HasMaxLength(20).HasColumnName("semestre");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.IdCurso)
                .HasConstraintName("matriculas_id_curso_fkey");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.IdEstudiante)
                .HasConstraintName("matriculas_id_estudiante_fkey");
        });

        modelBuilder.Entity<Profesore>(entity =>
        {
            entity.HasKey(e => e.IdProfesor).HasName("profesores_pkey");
            entity.ToTable("profesores");

            entity.Property(e => e.IdProfesor).HasColumnName("id_profesor");
            entity.Property(e => e.Correo).HasMaxLength(100).HasColumnName("correo");
            entity.Property(e => e.Especialidad).HasMaxLength(100).HasColumnName("especialidad");
            entity.Property(e => e.Nombre).HasMaxLength(100).HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuarios_pkey");
            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Username, "usuarios_username_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Passwordhash).HasMaxLength(255).HasColumnName("passwordhash");
            entity.Property(e => e.Role).HasMaxLength(20).HasColumnName("role");
            entity.Property(e => e.Username).HasMaxLength(50).HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
