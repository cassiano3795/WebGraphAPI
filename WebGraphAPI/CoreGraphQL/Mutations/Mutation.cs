using System.Threading.Tasks;
using BD.Models;
using CoreGraphQL.Resolvers;
using GraphQL;
using GraphQL.Types;

namespace CoreGraphQL.Mutations
{
    public class Mutation
    {
        private readonly UserResolvers _userResolvers;
        private readonly ClientResolvers _clientResolvers;

        public Mutation(UserResolvers userResolvers, ClientResolvers clientResolvers)
        {
            _userResolvers = userResolvers;
            _clientResolvers = clientResolvers;
        }

        // CLIENT
        [GraphQLMetadata("createClient")]
        public Clientes CreateClient(ResolveFieldContext context)
        {

            var user = _clientResolvers.CreateClient(context);
            return user;
        }

        [GraphQLMetadata("updateClient")]
        public Clientes UpdateClient(ResolveFieldContext context)
        {

            var user = _clientResolvers.UpdateClient(context);
            return user;
        }

        [GraphQLMetadata("deleteClient")]
        public bool DeleteClient(ResolveFieldContext context)
        {

            var user = _clientResolvers.DeleteClient(context);
            return user;
        }

        // USER
        [GraphQLMetadata("createUser")]
        public async Task<Usuarios> CreateUser(ResolveFieldContext context)
        {

            var user = await _userResolvers.CreateUser(context);
            return user;
        }

        [GraphQLMetadata("updateUser")]
        public Usuarios UpdateUser(ResolveFieldContext context)
        {

            var user = _userResolvers.UpdateUser(context);
            return user;
        }

        [GraphQLMetadata("deleteUser")]
        public bool DeleteUser(ResolveFieldContext context)
        {

            var user = _userResolvers.DeleteUser(context);
            return user;
        }
    }
}
