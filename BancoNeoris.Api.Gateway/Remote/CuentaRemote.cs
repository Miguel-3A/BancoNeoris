using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Gateway.Remote
{
    public class CuentaRemote
    {
        public Guid? cuentaId { get; set; }

        public string numeroCuenta { get; set; }

        public string tipoCuenta { get; set; }

        public int saldoInicial { get; set; }

        public bool estadoCuenta { get; set; }

        public Guid? clienteId { get; set; }
    }
}
