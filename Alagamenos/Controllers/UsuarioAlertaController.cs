using Alagamenos.DbConfig;
using Alagamenos.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Alagamenos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioAlertaController : ControllerBase
{
    private readonly AlagamenosDbContext _context;

    public UsuarioAlertaController(AlagamenosDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Relaciona um alerta a um usuário", 
        Description = "Cria uma nova entrada na tabela de relacionamento N:N")]
    public async Task<IActionResult> Post([FromBody] UsuarioAlerta usuarioAlerta)
    {
        _context.UsuarioAlertas.Add(usuarioAlerta);
        await _context.SaveChangesAsync();
        return Ok(usuarioAlerta);
    }

    [HttpGet("por-usuario/{usuarioId}")]
    [SwaggerOperation(Summary = "Retorna uma lista de alertas por usuário", 
        Description = "Cria uma lista de alertas por usuário")]
    public async Task<IActionResult> GetAlertasPorUsuario(int usuarioId)
    {
        var alertas = await _context.UsuarioAlertas
            .Where(ua => ua.UsuarioId == usuarioId)
            .Select(ua => ua.Alerta)
            .ToListAsync();

        return Ok(alertas);
    }

    [HttpDelete]
    [SwaggerOperation(Summary = "Deleta um alerta vinculado a um usuário", 
        Description = "Remove um alerta vinculado a um usuário")]
    public async Task<IActionResult> Delete([FromBody] UsuarioAlerta usuarioAlerta)
    {
        _context.UsuarioAlertas.Remove(usuarioAlerta);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}