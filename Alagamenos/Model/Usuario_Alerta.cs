using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alagamenos.Model;

[Table("USUARIO_ALERTA")]
public class Usuario_Alerta
{
    [Key, Column("USUARIO_ID", Order = 0)]
    [Description("FK para o Usuário que recebeu o alerta")]
    public int UsuarioId { get; set; }

    [Key, Column("ALERTA_ID", Order = 1)]
    [Description("FK para o Alerta que foi recebido")]
    public int AlertaId { get; set; }

    [ForeignKey("UsuarioId")]
    public Usuario Usuario { get; set; }

    [ForeignKey("AlertaId")]
    public Alerta Alerta { get; set; }
}