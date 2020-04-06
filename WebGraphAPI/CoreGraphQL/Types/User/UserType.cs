using BD.Models;
using CoreGraphQL.Resolvers;
using GraphQL;
using GraphQL.Types;

namespace CoreGraphQL.Types.User
{
    [GraphQLMetadata("User", IsTypeOf = typeof(Usuarios))]
    public class UserType
    {
        private readonly ClientResolvers _clientResolvers;

        public UserType(ClientResolvers clientResolvers)
        {
            _clientResolvers = clientResolvers;
        }

        public int Id(Usuarios user) => user.Idusuario;
        public string Nome(Usuarios user) => user.Nome;
        public string Email(Usuarios user) => user.Email;
        public Clientes Client(ResolveFieldContext context)
        {
            return _clientResolvers.ResolveClient(context);
        }
    }
}
