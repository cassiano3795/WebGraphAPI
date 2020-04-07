using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BD.Models;
using GraphQL;

namespace CoreGraphQL.Types.Client
{
    [GraphQLMetadata("ClientInput", IsTypeOf = typeof(Clientes))]
    public class ClientInputType
    {
        public string Nome(Clientes cliente) => cliente.Nome;
        public string Email(Clientes cliente) => cliente.Email;

        public int[] Users(Clientes cliente)
        {
            return cliente.Usuarios.Select(s => s.Idusuario).ToArray();
        }
    }
}
