using BancoNeoris.Api.Gateway.Interface;
using BancoNeoris.Api.Gateway.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Gateway.Implement
{
    public class CuentaImplement : ICuentaRemote
    {
        private readonly IHttpClientFactory _httpClient;

        public CuentaImplement(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool resultado, CuentaRemote cuenta, HttpResponseMessage respuesta)> ConsultaCuenta(Guid cuentaId)
        {
            try
            {
                var cliente = _httpClient.CreateClient("GatewayServicio");
                var response = await cliente.GetAsync($"/Cuenta/{cuentaId}");
                if (response.IsSuccessStatusCode)
                {
                    var resultado = JsonSerializer.Deserialize<RespuestaRemote<CuentaRemote>>(response.Content.ReadAsStringAsync().Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return (true, resultado.data, response);
                }
                return (false, null, response);
            }
            catch (Exception e)
            {
                return (false, null, new HttpResponseMessage() { ReasonPhrase = e.Message });
            }
        }

        public async Task<(bool resultado, CuentaRemote cuenta, HttpResponseMessage respuesta)> ActualizaCuenta(CuentaRemote cuenta)
        {
            try
            {
                
                var cliente = _httpClient.CreateClient("GatewayServicio");
                var content = new StringContent(JsonSerializer.Serialize(cuenta), Encoding.UTF8, "application/json");
                var response = await cliente.PatchAsync($"/Cuenta", content);

                if (response.IsSuccessStatusCode)
                {
                    var resultado = JsonSerializer.Deserialize<RespuestaRemote<CuentaRemote>>(response.Content.ReadAsStringAsync().Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return (true, resultado.data, response);
                }
                return (false, null, response);
            }
            catch (Exception e)
            {
                return (false, null, new HttpResponseMessage() { ReasonPhrase = e.Message });
            }
        }

        public async Task<(bool resultado, List<CuentaRemote> cuentas, HttpResponseMessage respuesta)> ConsultaCuentasCliente(Guid clienteId)
        {
            try
            {
                var cliente = _httpClient.CreateClient("GatewayServicio");
                var response = await cliente.GetAsync($"/CuentaCliente/{clienteId}");
                if (response.IsSuccessStatusCode)
                {
                    var resultado = JsonSerializer.Deserialize<RespuestaRemote<List<CuentaRemote>>>(response.Content.ReadAsStringAsync().Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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
