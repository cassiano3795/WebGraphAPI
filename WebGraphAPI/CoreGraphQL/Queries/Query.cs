using BD.Models;
using CoreGraphQL.Resolvers;
using CoreGraphQL.Types.User;
using GraphQL;
using GraphQL.Types;

namespace CoreGraphQL.Queries
{
    public class Query
    {
        private readonly UserResolvers _userResolvers;
        private readonly ClientResolvers _clientResolvers;

        public Query(UserResolvers userResolvers, ClientResolvers clientResolvers)
        {
            _userResolvers = userResolvers;
            _clientResolvers = clientResolvers;
        }

        [GraphQLMetadata("user")]
        public Usuarios GetUser(ResolveFieldContext context, int id)
        {
            var user = _userResolvers.ResolveUser(context, id);
            return user;

        }

        [GraphQLMetadata("users")]
        public UserListType GetUsers(ResolveFieldContext context, int? id, int size, int offSet)
        {
            var users = _userResolvers.ResolveUsers(context, id, size, offSet, out var count);
            var userListType = new UserListType
            {
                Users = users,
                Count = count
            };

            return userListType;
        }

        [GraphQLMetadata("client")]
        public Clientes GetClient(ResolveFieldContext context)
        {
            return _clientResolvers.ResolveClient(context);
        }
    }
}
