using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Cliente.Persistencia
{
    public class ContextoCliente : DbContext
    {
        public ContextoCliente() { }

        public ContextoCliente(DbContextOptions<ContextoCliente> options) : base(options) { }

        public virtual DbSet<Model.Cliente> Cliente { get; set; }
    }
}
