using Microsoft.AspNetCore.Mvc;
using tareaSem9.Core.Domain.Entities;
using tareaSem9.Core.Ports.Inbound;

namespace tareaSem9.Adapters.Inbound.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursosController : ControllerBase
{
    private readonly ICursoUseCase _cursoUseCase;

    public CursosController(ICursoUseCase cursoUseCase)
    {
        _cursoUseCase = cursoUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cursos = await _cursoUseCase.GetAllAsync();
        return Ok(cursos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var curso = await _cursoUseCase.GetByIdAsync(id);
        if (curso == null)
            return NotFound(new { message = $"Curso con id {id} no encontrado." });

        return Ok(curso);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Curso curso)
    {
        var nuevo = await _cursoUseCase.CreateAsync(curso);
        return CreatedAtAction(nameof(GetById), new { id = nuevo.IdCurso }, nuevo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Curso curso)
    {
        var actualizado = await _cursoUseCase.UpdateAsync(id, curso);
        if (!actualizado)
            return NotFound(new { message = $"Curso con id {id} no encontrado." });

        return Ok(new { message = "Curso actualizado con éxito." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _cursoUseCase.DeleteAsync(id);
        if (!eliminado)
            return NotFound(new { message = $"Curso con id {id} no encontrado." });

        return NoContent();
    }
}
