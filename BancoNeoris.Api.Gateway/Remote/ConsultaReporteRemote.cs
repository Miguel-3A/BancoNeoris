using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Gateway.Remote
{
    public class ConsultaReporteRemote
    {
        public int clienteIdentificacion { get; set; }

        public string fechaInicial { get; set; }

        public string fechaFinal { get; set; }
    }
}
