using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Alagamenos.Model;

[Table("CIDADE")]
public class Cidade : IBindableFromHttpContext<Cidade>
{
    public static async ValueTask<Cidade?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        if (context.Request.ContentType.Contains("xml"))
        {
            var xmlDoc = await XDocument.LoadAsync(context.Request.Body, LoadOptions.None, context.RequestAborted);
            var serializer = new XmlSerializer(typeof(Estado));
            return (Cidade?)serializer.Deserialize(xmlDoc.CreateReader());
        }

        return await context.Request.ReadFromJsonAsync<Cidade>();
    }
    
    [Column("ID")]
    [Key]
    [Description("Identificador único da cidade")]
    public int Id { get; set; }
    
    [Column("NOME_CIDADE")]
    [Description("Nome da cidade")]
    public string NomeCidade { get; set; }
    
    [Column("ESTADO_ID")]
    [Description("Identificador único do Estado em que se encontra a Cidade")]
    public int EstadoId { get; set; }
    
    [ForeignKey("EstadoId")]
    public Estado Estado { get; set; }
}