using BancoNeoris.Api.Gateway.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Gateway.Interface
{
    public interface IMovimientoRemote
    {
        Task<(bool resultado, List<MovimientoRemote> movimientos, HttpResponseMessage respuesta)> ConsultaMovimientoFecha(Guid cuentaId, string fechaInicial, string fechaFinal);
    }
}
