using BancoNeoris.Api.Gateway.Interface;
using BancoNeoris.Api.Gateway.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Gateway.Handler
{
    public class TransaccionHandler : DelegatingHandler
    {
        private readonly ICuentaRemote _cuentaRemote;

        public TransaccionHandler(ICuentaRemote cuentaRemote)
        {
            _cuentaRemote = cuentaRemote;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage msgRta = new HttpResponseMessage();

            var dataRequest = JsonSerializer.Deserialize<MovimientoRemote>(request.Content.ReadAsStringAsync().Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var cuenta = await _cuentaRemote.ConsultaCuenta(dataRequest.cuentaId ?? Guid.Empty);

            if (cuenta.cuenta != null)
            {
                dataRequest.saldoMovimiento = dataRequest.tipoMovimiento.ToUpper().Contains("DEPOSITO") ? cuenta.cuenta.saldoInicial + dataRequest.valorMovimiento : cuenta.cuenta.saldoInicial - dataRequest.valorMovimiento;
                request.Content = new StringContent(JsonSerializer.Serialize(dataRequest), System.Text.Encoding.UTF8, "application/json");

                msgRta = await base.SendAsync(request, cancellationToken);

                if (msgRta.IsSuccessStatusCode)
                {
                    cuenta.cuenta.saldoInicial = dataRequest.saldoMovimiento;
                    await _cuentaRemote.ActualizaCuenta(cuenta.cuenta);
                }
            }
            else 
            {
                msgRta = cuenta.respuesta;
            }

            return msgRta;
        }
    }
}
