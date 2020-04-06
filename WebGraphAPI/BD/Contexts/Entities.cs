using BD.Models;
using Microsoft.EntityFrameworkCore;

namespace BD.Contexts
{
    public partial class Entities : DbContext
    {
        public Entities()
        {
        }

        public Entities(DbContextOptions<Entities> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }

    }
}
