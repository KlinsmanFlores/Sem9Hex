using Microsoft.AspNetCore.Mvc;
using tareaSem9.Core.Domain.Entities;
using tareaSem9.Core.Ports.Inbound;

namespace tareaSem9.Adapters.Inbound.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfesoresController : ControllerBase
{
    private readonly IProfesoreUseCase _profesoreUseCase;

    public ProfesoresController(IProfesoreUseCase profesoreUseCase)
    {
        _profesoreUseCase = profesoreUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var profesores = await _profesoreUseCase.GetAllAsync();
        return Ok(profesores);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var profesor = await _profesoreUseCase.GetByIdAsync(id);
        if (profesor == null)
            return NotFound(new { message = $"Profesor con id {id} no encontrado." });

        return Ok(profesor);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Profesore profesor)
    {
        var nuevo = await _profesoreUseCase.CreateAsync(profesor);
        return CreatedAtAction(nameof(GetById), new { id = nuevo.IdProfesor }, nuevo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Profesore profesor)
    {
        var actualizado = await _profesoreUseCase.UpdateAsync(id, profesor);
        if (!actualizado)
            return NotFound(new { message = $"Profesor con id {id} no encontrado." });

        return Ok(new { message = "Profesor actualizado con éxito." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _profesoreUseCase.DeleteAsync(id);
        if (!eliminado)
            return NotFound(new { message = $"Profesor con id {id} no encontrado." });

        return NoContent();
    }
}
