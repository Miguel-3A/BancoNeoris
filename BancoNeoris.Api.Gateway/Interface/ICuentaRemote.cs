using BancoNeoris.Api.Gateway.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Gateway.Interface
{
    public interface ICuentaRemote
    {
        Task<(bool resultado, CuentaRemote cuenta, HttpResponseMessage respuesta)> ConsultaCuenta(Guid cuentaId);
        Task<(bool resultado, CuentaRemote cuenta, HttpResponseMessage respuesta)> ActualizaCuenta(CuentaRemote cuenta);
        Task<(bool resultado, List<CuentaRemote> cuentas, HttpResponseMessage respuesta)> ConsultaCuentasCliente(Guid clienteId);
    }
}
