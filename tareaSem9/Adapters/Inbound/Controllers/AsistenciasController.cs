using Microsoft.AspNetCore.Mvc;
using tareaSem9.Core.Domain.Entities;
using tareaSem9.Core.Ports.Inbound;

namespace tareaSem9.Adapters.Inbound.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AsistenciasController : ControllerBase
{
    private readonly IAsistenciaUseCase _asistenciaUseCase;

    public AsistenciasController(IAsistenciaUseCase asistenciaUseCase)
    {
        _asistenciaUseCase = asistenciaUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var asistencias = await _asistenciaUseCase.GetAllAsync();
        return Ok(asistencias);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var asistencia = await _asistenciaUseCase.GetByIdAsync(id);
        if (asistencia == null)
            return NotFound(new { message = $"Asistencia con id {id} no encontrada." });

        return Ok(asistencia);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Asistencia asistencia)
    {
        var nueva = await _asistenciaUseCase.CreateAsync(asistencia);
        return CreatedAtAction(nameof(GetById), new { id = nueva.IdAsistencia }, nueva);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Asistencia asistencia)
    {
        var actualizada = await _asistenciaUseCase.UpdateAsync(id, asistencia);
        if (!actualizada)
            return NotFound(new { message = $"Asistencia con id {id} no encontrada." });

        return Ok(new { message = "Asistencia actualizada con éxito." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminada = await _asistenciaUseCase.DeleteAsync(id);
        if (!eliminada)
            return NotFound(new { message = $"Asistencia con id {id} no encontrada." });

        return NoContent();
    }
}
