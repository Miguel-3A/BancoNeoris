using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Gateway.Remote
{
    public class ReporteResponse
    {
        public DateTime? fechaMovimiento { get; set; }

        public string nombre { get; set; }

        public string numeroCuenta { get; set; }

        public string tipoCuenta { get; set; }

        public int saldoInicial { get; set; }

        public bool estadoCuenta { get; set; }

        public int valorMovimiento { get; set; }

        public int saldoDisponible { get; set; }
    }
}
