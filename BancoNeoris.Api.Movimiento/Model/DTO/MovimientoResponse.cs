using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Movimiento.Model.DTO
{
    public class MovimientoResponse
    {
        public DateTime? fechaMovimiento { get; set; }

        public string tipoMovimiento { get; set; }

        public int valorMovimiento { get; set; }

        public int saldoMovimiento { get; set; }

        public Guid? cuentaId { get; set; }
    }
}
