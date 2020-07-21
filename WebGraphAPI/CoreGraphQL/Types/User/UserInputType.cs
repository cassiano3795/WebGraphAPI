using BD.Models;
using GraphQL;

namespace CoreGraphQL.Types.User
{
    [GraphQLMetadata("UserInput", IsTypeOf = typeof(Usuarios))]
    public class UserInputType
    {
        public string Nome(Usuarios user) => user.Nome;
        public string Email(Usuarios user) => user.Email;
        public int IdCliente(Usuarios user) => user.Idcliente;
    }
}
