using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Alagamenos.Model;

[Table("BAIRRO")]
public class Bairro : IBindableFromHttpContext<Bairro>
{
    public static async ValueTask<Bairro?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        if (context.Request.ContentType.Contains("xml"))
        {
            var xmlDoc = await XDocument.LoadAsync(context.Request.Body, LoadOptions.None, context.RequestAborted);
            var serializer = new XmlSerializer(typeof(Bairro));
            return (Bairro?)serializer.Deserialize(xmlDoc.CreateReader());
        }

        return await context.Request.ReadFromJsonAsync<Bairro>();
    }
    
    [Column("ID")]
    [Key]
    [Description("Identificador único do Bairro")]
    public int Id { get; set; }
    
    [Column("NOME_BAIRRO")]
    [Description("Nome do Bairro")]
    public string NomeBairro { get; set; }
    
    [Column("CIDADE_ID")]
    [Description("Identificador único da Cidade em que se encontra o Bairro")]
    public int CidadeId { get; set; }
    
    [ForeignKey("CidadeId")]
    public Cidade Cidade { get; set; }
}