using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Alagamenos.Dto;

[SwaggerSchema("DTO usado para criar um novo bairro")]
public class BairroDto
{
    [Required]
    [SwaggerSchema("Identificador único do bairro")]
    public int Id { get; set; }
    
    [Required]
    [SwaggerSchema("Nome do bairro")]
    public string NomeBairro { get; set; }
    
    [Required]
    [SwaggerSchema("Identificador único da cidade em que se encontra o bairro")]
    public int CidadeId { get; set; }
}