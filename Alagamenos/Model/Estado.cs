using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Alagamenos.Model;

[Table("ESTADO")]
public class Estado : IBindableFromHttpContext<Estado>
{
    public static async ValueTask<Estado?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        if (context.Request.ContentType.Contains("xml"))
        {
            var xmlDoc = await XDocument.LoadAsync(context.Request.Body, LoadOptions.None, context.RequestAborted);
            var serializer = new XmlSerializer(typeof(Estado));
            return (Estado?)serializer.Deserialize(xmlDoc.CreateReader());
        }

        return await context.Request.ReadFromJsonAsync<Estado>();
    }
    
    [Column("ID")]
    [Key]
    [Description("Identificador único de Estado")]
    public int Id { get; set; }
    
    [Column("NOME_ESTADO")]
    [Description("Nome do estado")]
    public string NomeEstado { get; set; }
    
}