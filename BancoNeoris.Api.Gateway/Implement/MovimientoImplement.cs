using BancoNeoris.Api.Gateway.Interface;
using BancoNeoris.Api.Gateway.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Gateway.Implement
{
    public class MovimientoImplement : IMovimientoRemote
    {
        private readonly IHttpClientFactory _httpClient;

        public MovimientoImplement(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool resultado, List<MovimientoRemote> movimientos, HttpResponseMessage respuesta)> ConsultaMovimientoFecha(Guid cuentaId, string fechaInicial, string fechaFinal)
        {
            try
            {
                var cliente = _httpClient.CreateClient("GatewayServicio");
                var response = await cliente.GetAsync($"/Movimiento/{cuentaId}/{fechaInicial}/{fechaFinal}");
                if (response.IsSuccessStatusCode)
                {
                    var resultado = JsonSerializer.Deserialize<RespuestaRemote<List<MovimientoRemote>>>(response.Content.ReadAsStringAsync().Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return (true, resultado.data, response);
                }
                return (false, null, response);
            }
            catch (Exception e)
            {
                return (false, null, new HttpResponseMessage() { ReasonPhrase = e.Message });
            }
        }        
    }
}
