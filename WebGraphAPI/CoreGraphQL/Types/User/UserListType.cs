using System.Collections.Generic;
using BD.Models;
using GraphQL;

namespace CoreGraphQL.Types.User
{
    [GraphQLMetadata("UserList")]
    public class UserListType
    {
        public List<Usuarios> Users { get; set; }
        public int Count { get; set; }
    }
}
