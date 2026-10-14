using Microsoft.AspNetCore.Mvc;
using tareaSem9.Core.Domain.Entities;
using tareaSem9.Core.Ports.Inbound;

namespace tareaSem9.Adapters.Inbound.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MateriasController : ControllerBase
{
    private readonly IMateriaUseCase _materiaUseCase;

    public MateriasController(IMateriaUseCase materiaUseCase)
    {
        _materiaUseCase = materiaUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var materias = await _materiaUseCase.GetAllAsync();
        return Ok(materias);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var materia = await _materiaUseCase.GetByIdAsync(id);
        if (materia == null)
            return NotFound(new { message = $"Materia con id {id} no encontrada." });

        return Ok(materia);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Materia materia)
    {
        var nueva = await _materiaUseCase.CreateAsync(materia);
        return CreatedAtAction(nameof(GetById), new { id = nueva.IdMateria }, nueva);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Materia materia)
    {
        var actualizada = await _materiaUseCase.UpdateAsync(id, materia);
        if (!actualizada)
            return NotFound(new { message = $"Materia con id {id} no encontrada." });

        return Ok(new { message = "Materia actualizada con éxito." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminada = await _materiaUseCase.DeleteAsync(id);
        if (!eliminada)
            return NotFound(new { message = $"Materia con id {id} no encontrada." });

        return NoContent();
    }
}
