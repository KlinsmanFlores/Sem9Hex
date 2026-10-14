using Microsoft.AspNetCore.Mvc;
using tareaSem9.Core.Domain.Entities;
using tareaSem9.Core.Ports.Inbound;

namespace tareaSem9.Adapters.Inbound.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EvaluacionesController : ControllerBase
{
    private readonly IEvaluacioneUseCase _evaluacioneUseCase;

    public EvaluacionesController(IEvaluacioneUseCase evaluacioneUseCase)
    {
        _evaluacioneUseCase = evaluacioneUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var evaluaciones = await _evaluacioneUseCase.GetAllAsync();
        return Ok(evaluaciones);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var evaluacion = await _evaluacioneUseCase.GetByIdAsync(id);
        if (evaluacion == null)
            return NotFound(new { message = $"Evaluación con id {id} no encontrada." });

        return Ok(evaluacion);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Evaluacione evaluacion)
    {
        var nueva = await _evaluacioneUseCase.CreateAsync(evaluacion);
        return CreatedAtAction(nameof(GetById), new { id = nueva.IdEvaluacion }, nueva);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Evaluacione evaluacion)
    {
        var actualizada = await _evaluacioneUseCase.UpdateAsync(id, evaluacion);
        if (!actualizada)
            return NotFound(new { message = $"Evaluación con id {id} no encontrada." });

        return Ok(new { message = "Evaluación actualizada con éxito." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminada = await _evaluacioneUseCase.DeleteAsync(id);
        if (!eliminada)
            return NotFound(new { message = $"Evaluación con id {id} no encontrada." });

        return NoContent();
    }
}
