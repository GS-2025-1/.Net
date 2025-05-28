using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Alagamenos.Model;

[Table("RUA")]
public class Rua : IBindableFromHttpContext<Rua>
{
    public static async ValueTask<Rua?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        if (context.Request.ContentType.Contains("xml"))
        {
            var xmlDoc = await XDocument.LoadAsync(context.Request.Body, LoadOptions.None, context.RequestAborted);
            var serializer = new XmlSerializer(typeof(Rua));
            return (Rua?)serializer.Deserialize(xmlDoc.CreateReader());
        }

        return await context.Request.ReadFromJsonAsync<Rua>();
    }
    
    [Column("ID")]
    [Key]
    [Description("Identificador único da Rua")]
    public int Id { get; set; }
    
    [Column("NOME_RUA")]
    [Description("Nome da Rua")]
    public string NomeRua { get; set; }
    
    [Column("OBSERVACAO")]
    [Description("Observação referente a Rua adicionada")]
    public string Observacao { get; set; }
    
    [Column("BAIRRO_ID")]
    [Description("Identificador único do Bairro em que se encontra a Rua")]
    public int BairroId { get; set; }
    
    [ForeignKey("BairroId")]
    public Bairro Bairro { get; set; }
}