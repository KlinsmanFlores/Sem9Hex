using Microsoft.AspNetCore.Mvc;
using tareaSem9.Core.Domain.Entities;
using tareaSem9.Core.Ports.Inbound;

namespace tareaSem9.Adapters.Inbound.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudiantesController : ControllerBase
{
    private readonly IEstudianteUseCase _estudianteUseCase;

    public EstudiantesController(IEstudianteUseCase estudianteUseCase)
    {
        _estudianteUseCase = estudianteUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var estudiantes = await _estudianteUseCase.GetAllAsync();
        return Ok(estudiantes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var estudiante = await _estudianteUseCase.GetByIdAsync(id);
        if (estudiante == null)
            return NotFound(new { message = $"Estudiante con id {id} no encontrado." });

        return Ok(estudiante);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Estudiante estudiante)
    {
        var nuevo = await _estudianteUseCase.CreateAsync(estudiante);
        return CreatedAtAction(nameof(GetById), new { id = nuevo.IdEstudiante }, nuevo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Estudiante estudiante)
    {
        var actualizado = await _estudianteUseCase.UpdateAsync(id, estudiante);
        if (!actualizado)
            return NotFound(new { message = $"Estudiante con id {id} no encontrado." });

        return Ok(new { message = "Estudiante actualizado con éxito." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _estudianteUseCase.DeleteAsync(id);
        if (!eliminado)
            return NotFound(new { message = $"Estudiante con id {id} no encontrado." });

        return NoContent();
    }
}
