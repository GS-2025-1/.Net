using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace Alagamenos.Model;

[Table("USUARIO")]
public class Usuario : IBindableFromHttpContext<Usuario>
{
    public static async ValueTask<Usuario?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        if (context.Request.ContentType.Contains("xml"))
        {
            var xmlDoc = await XDocument.LoadAsync(context.Request.Body, LoadOptions.None, context.RequestAborted);
            var serializer = new XmlSerializer(typeof(Estado));
            return (Usuario?)serializer.Deserialize(xmlDoc.CreateReader());
        }

        return await context.Request.ReadFromJsonAsync<Usuario>();
    }
    [Column("ID")]
    [Key]
    [Description("Identificador único do Usuário")]
    public int Id { get; set; }

    [Column("NOME")]
    [Description("Nome completo do Usuário")]
    public string Nome { get; set; }

    [Column("DATA_NASCIMENTO")]
    [Description("Data de nascimento do Usuário")]
    public DateTime DataNascimento { get; set; }

    [Column("TELEFONE")]
    [Description("Telefone de contato do Usuário")]
    public string Telefone { get; set; }

    [Column("EMAIL")]
    [Description("Email do Usuário")]
    public string Email { get; set; }
    
    [Column("SENHA")]
    [Description("Senha do Usuário")]
    public string Senha { get; set; }
    
    public ICollection<UsuarioAlerta> UsuarioAlertas { get; set; }
}