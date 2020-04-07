using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BD.Models
{
    public partial class Clientes
    {
        [Key]
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Site { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
