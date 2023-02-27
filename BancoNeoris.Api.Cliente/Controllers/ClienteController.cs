using BancoNeoris.Api.Cliente.Aplicacion;
using BancoNeoris.Api.Cliente.Negocio;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Cliente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClienteController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Model.Respuesta<Model.DTO.ClienteResponse>>> Crear(NuevoCliente.ClienteNuevo data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<Model.Respuesta<List<Model.DTO.ClienteResponse>>>> Consultar()
        {
            return await _mediator.Send(new ConsultaClientes.Ejecuta());
        }

        [HttpGet("{clienteId}")]
        public async Task<ActionResult<Model.Respuesta<Model.DTO.ClienteResponse>>> Consultar(string clienteId)
        {
            return await _mediator.Send(new ConsultaClienteId.Ejecuta() { clienteId = clienteId });
        }

        [HttpPatch]
        public async Task<ActionResult<Model.Respuesta<Model.DTO.ClienteResponse>>> Editar(EditarCliente.ModificarCliente data)
        {
            return await _mediator.Send(data);
        }

        [HttpDelete("{clienteId}")]
        public async Task<ActionResult<Model.Respuesta<Model.DTO.ClienteResponse>>> Eliminar(string clienteId)
        {
            return await _mediator.Send(new EliminarCliente.EliminaCliente() { clienteId = clienteId });
        }

        [HttpPost("/api/ClienteIdentificacion")]
        public async Task<ActionResult<Model.Respuesta<Model.DTO.ClienteResponse>>> ConsultarClienteReporte(ConsultaIdentificacionCliente.ConsultaCliente data)
        {
            return await _mediator.Send(data);
        }
    }
}
