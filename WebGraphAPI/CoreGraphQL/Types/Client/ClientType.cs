using BD.Models;
using CoreGraphQL.Resolvers;
using CoreGraphQL.Types.User;
using GraphQL;
using GraphQL.Types;

namespace CoreGraphQL.Types.Client
{
    [GraphQLMetadata("Client", IsTypeOf = typeof(Clientes))]
    public class ClientType
    {
        private readonly UserResolvers _userResolvers;

        public ClientType(UserResolvers userResolvers)
        {
            _userResolvers = userResolvers;
        }

        public int Id(Clientes client) => client.IdCliente;
        public string Nome(Clientes client) => client.Nome;
        public string Site(Clientes client) => client.Site;

        public UserListType Users(ResolveFieldContext context, int? id, int size, int offSet)
        {
            var users = _userResolvers.ResolveUsers(context, id, size, offSet, out var count);
            var userListType = new UserListType
            {
                Users = users,
                Count = count
            };

            return userListType;
        }
    }
}
