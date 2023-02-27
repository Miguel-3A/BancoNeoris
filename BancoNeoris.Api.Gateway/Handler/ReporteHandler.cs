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
    public class ReporteHandler : DelegatingHandler
    {
        private readonly ICuentaRemote _cuentaRemote;
        private readonly IMovimientoRemote _movimientoRemote;

        public ReporteHandler(ICuentaRemote cuentaRemote, IMovimientoRemote movimientoRemote)
        {
            _cuentaRemote = cuentaRemote;
            _movimientoRemote = movimientoRemote;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage msgRta = new HttpResponseMessage();

            msgRta = await base.SendAsync(request, cancellationToken);
            if (msgRta.IsSuccessStatusCode)
            {
                var cliente = JsonSerializer.Deserialize<RespuestaRemote<ClienteRemote>>(msgRta.Content.ReadAsStringAsync().Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var listaCuentas = await _cuentaRemote.ConsultaCuentasCliente(cliente.data.clienteId ?? Guid.Empty);

                if (listaCuentas.cuentas != null && listaCuentas.cuentas.Count > 0)
                {
                    List<ReporteResponse> listaReporte = new List<ReporteResponse>();

                    foreach (var cuenta in listaCuentas.cuentas)
                    {
                        var dataRequest = JsonSerializer.Deserialize<ConsultaReporteRemote>(request.Content.ReadAsStringAsync().Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        var movimientos = await _movimientoRemote.ConsultaMovimientoFecha(cuenta.cuentaId ?? Guid.Empty, dataRequest.fechaInicial, dataRequest.fechaFinal);

                        foreach (var movimiento in movimientos.movimientos)
                        {
                            ReporteResponse movimientoReporte = new ReporteResponse()
                            {
                                fechaMovimiento = movimiento.fechaMovimiento,
                                nombre = cliente.data.nombre,
                                numeroCuenta = cuenta.numeroCuenta,
                                tipoCuenta = cuenta.tipoCuenta,
                                saldoInicial = movimiento.tipoMovimiento.ToUpper().Contains("DEPOSITO") ? movimiento.saldoMovimiento - movimiento.valorMovimiento : movimiento.saldoMovimiento + movimiento.valorMovimiento,
                                estadoCuenta = cuenta.estadoCuenta,
                                valorMovimiento = movimiento.tipoMovimiento.ToUpper().Contains("DEPOSITO") ? movimiento.valorMovimiento : -movimiento.valorMovimiento,
                                saldoDisponible = movimiento.saldoMovimiento
                            };
                            listaReporte.Add(movimientoReporte);
                        }
                    }
                    msgRta.Content = new StringContent(JsonSerializer.Serialize(listaReporte), System.Text.Encoding.UTF8, "application/json");
                }
                else
                {
                    msgRta = listaCuentas.respuesta;
                }
            }
            return msgRta;
        }
    }
}
