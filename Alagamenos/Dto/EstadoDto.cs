using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Alagamenos.Dto;

[SwaggerSchema("DTO usado para criar um novo estado")]
public class EstadoDto
{
    [Required]
    [SwaggerSchema("Identificador único de estado")]
    public int Id { get; set; }
    
    [Required]
    [SwaggerSchema("Nome do estado")]
    public string NomeEstado { get; set; }
}