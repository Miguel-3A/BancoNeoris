using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Movimiento.Persistencia
{
    public class ContextoMovimiento : DbContext
    {
        public ContextoMovimiento(DbContextOptions<ContextoMovimiento> options) : base(options) { }

        public DbSet<Model.Movimiento> Movimiento { get; set; }
    }
}
