using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Alagamenos.Model;

[Table("ALERTAS")]
[SwaggerSchema("Tabela que representa os alertas de alagamento")]
public class Alerta : IBindableFromHttpContext<Alerta>
{
    public static async ValueTask<Alerta?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        if (context.Request.ContentType.Contains("xml"))
        {
            var xmlDoc = await XDocument.LoadAsync(context.Request.Body, LoadOptions.None, context.RequestAborted);
            var serializer = new XmlSerializer(typeof(Alerta));
            return (Alerta?)serializer.Deserialize(xmlDoc.CreateReader());
        }

        return await context.Request.ReadFromJsonAsync<Alerta>();
    }

    [Key]
    [Column("ID")]
    [SwaggerSchema("Identificador único do Alerta", ReadOnly = true)]
    public int Id { get; set; }

    [Required]
    [Column("MENSAGEM")]
    [SwaggerSchema("Mensagem do Alerta emitido")]
    public string Mensagem { get; set; }

    [Column("DATA_CRIACAO")]
    [SwaggerSchema("Data e hora em que o alerta foi criado")]
    public DateTime DataCriacao { get; set; }

    [Column("RUA_ID")]
    [SwaggerSchema("Identificador da rua onde o alerta foi registrado")]
    public int RuaId { get; set; }

    [ForeignKey("RuaId")]
    [SwaggerSchema("Rua associada ao alerta", ReadOnly = true)]
    public Rua Rua { get; set; }

    [SwaggerSchema("Lista de relações entre este alerta e os usuários que o receberam", ReadOnly = true)]
    [JsonIgnore]
    public ICollection<UsuarioAlerta> UsuarioAlertas { get; set; }
}