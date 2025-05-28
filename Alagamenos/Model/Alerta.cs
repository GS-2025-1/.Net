using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Alagamenos.Model;

[Table("ALERTAS")]
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
    
    [Column("ID")]
    [Key]
    [Description("Identificador único do Alerta")]
    public int Id { get; set; }

    [Column("MENSAGEM")]
    [Description("Mensagem do Alerta emitido")]
    public string Mensagem { get; set; }

    [Column("DATA_CRIACAO")]
    [Description("Data e hora em que o alerta foi criado")]
    public DateTime DataCriacao { get; set; }

    [Column("RUA_ID")]
    [Description("Identificador da rua onde o alerta foi registrado")]
    public int RuaId { get; set; }
    
    [ForeignKey("RuaId")]
    public Rua rua { get; set; }

}