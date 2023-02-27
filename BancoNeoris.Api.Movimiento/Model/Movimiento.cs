using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Movimiento.Model
{
    public class Movimiento
    {
        public Guid? movimientoId { get; set; }

        public DateTime? fechaMovimiento { get; set; }

        public string tipoMovimiento { get; set; }

        public int valorMovimiento { get; set; }

        public int saldoMovimiento { get; set; }

        public Guid? cuentaId { get; set; }
    }
}
