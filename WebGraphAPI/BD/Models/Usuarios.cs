using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BD.Models
{
    public partial class Usuarios
    {
        [Key]
        public int Idusuario { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public bool? Ativo { get; set; }
        public bool? Excluido { get; set; }
        public string Nome { get; set; }

        [ForeignKey("Clientes")]
        public int Idcliente { get; set; }
        public virtual Clientes Clientes { get; set; }
    }
}
