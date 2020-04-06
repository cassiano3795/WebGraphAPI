namespace BD.Models
{
    public partial class Usuarios
    {
        public int Idusuario { get; set; }
        public int Idcliente { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public bool? Ativo { get; set; }
        public bool? Excluido { get; set; }
        public string Nome { get; set; }
        public virtual Clientes ClientNavigation { get; set; }
    }
}
