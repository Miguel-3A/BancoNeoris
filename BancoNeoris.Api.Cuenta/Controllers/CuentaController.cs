using BancoNeoris.Api.Cuenta.Negocio;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Cuenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CuentaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Model.Respuesta<Model.DTO.CuentaResponse>>> Crear(NuevaCuenta.CuentaNueva data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<Model.Respuesta<List<Model.DTO.CuentaResponse>>>> Consultar()
        {
            return await _mediator.Send(new ConsultaCuentas.Ejecuta());
        }

        [HttpGet("{cuentaId}")]
        public async Task<ActionResult<Model.Respuesta<Model.DTO.CuentaResponse>>> Consultar(string cuentaId)
        {
            return await _mediator.Send(new ConsultaCuentaId.Ejecuta() { cuentaId = cuentaId });
        }

        [HttpPatch]
        public async Task<ActionResult<Model.Respuesta<Model.DTO.CuentaResponse>>> Editar(EditarCuenta.ModificarCuenta data)
        {
            return await _mediator.Send(data);
        }

        [HttpDelete("{cuentaId}")]
        public async Task<ActionResult<Model.Respuesta<Model.DTO.CuentaResponse>>> Eliminar(string cuentaId)
        {
            return await _mediator.Send(new EliminarCuenta.EliminaCuenta() { cuentaId = cuentaId });
        }

        [HttpGet("/api/CuentaCliente/{clienteId}")]
        public async Task<ActionResult<Model.Respuesta<List<Model.DTO.CuentaResponse>>>> ConsultarPorCliente(string clienteId)
        {
            return await _mediator.Send(new ConsultaCuentaClienteId.Ejecuta() { clienteId = clienteId });
        }
    }
}
