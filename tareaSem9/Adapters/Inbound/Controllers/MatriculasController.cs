using Microsoft.AspNetCore.Mvc;
using tareaSem9.Core.Domain.Entities;
using tareaSem9.Core.Ports.Inbound;

namespace tareaSem9.Adapters.Inbound.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatriculasController : ControllerBase
{
    private readonly IMatriculaUseCase _matriculaUseCase;

    public MatriculasController(IMatriculaUseCase matriculaUseCase)
    {
        _matriculaUseCase = matriculaUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var matriculas = await _matriculaUseCase.GetAllAsync();
        return Ok(matriculas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var matricula = await _matriculaUseCase.GetByIdAsync(id);
        if (matricula == null)
            return NotFound(new { message = $"Matrícula con id {id} no encontrada." });

        return Ok(matricula);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Matricula matricula)
    {
        var nuevo = await _matriculaUseCase.CreateAsync(matricula);
        return CreatedAtAction(nameof(GetById), new { id = nuevo.IdMatricula }, nuevo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Matricula matricula)
    {
        var actualizado = await _matriculaUseCase.UpdateAsync(id, matricula);
        if (!actualizado)
            return NotFound(new { message = $"Matrícula con id {id} no encontrada." });

        return Ok(new { message = "Matrícula actualizada con éxito." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _matriculaUseCase.DeleteAsync(id);
        if (!eliminado)
            return NotFound(new { message = $"Matrícula con id {id} no encontrada." });

        return NoContent();
    }
}
