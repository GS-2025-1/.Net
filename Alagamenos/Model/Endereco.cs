using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Alagamenos.Model;

[Table("ENDERECO")]
public class Endereco : IBindableFromHttpContext<Endereco>
{
    public static async ValueTask<Endereco?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        if (context.Request.ContentType.Contains("xml"))
        {
            var xmlDoc = await XDocument.LoadAsync(context.Request.Body, LoadOptions.None, context.RequestAborted);
            var serializer = new XmlSerializer(typeof(Endereco));
            return (Endereco?)serializer.Deserialize(xmlDoc.CreateReader());
        }

        return await context.Request.ReadFromJsonAsync<Endereco>();
    }
    
    [Column("ID")]
    [Key]
    [Description("Identificador único da Rua")]
    public int Id { get; set; }
    
    [Column("NUMERO_ENDERECO")]
    [Description("Número do Endereço")]
    public string NumeroEndereco { get; set; }
    
    [Column("COMPLEMENTO")]
    [Description("Complemento do Endereço")]
    public string Complemento { get; set; }

    [Column("RUA_ID")]
    [Description("Identificador único da Rua em que se encontra o Endereço")]
    public int RuaId { get; set; }
    
    [ForeignKey("RuaId")]
    public Rua rua { get; set; }
    
    [Column("USUARIO_ID")]
    [Description("Identificador único do Usuário associado ao endereço")]
    public int UsuarioId { get; set; }
    
    [ForeignKey("UsuarioId")]
    public Usuario usuario { get; set; }
}