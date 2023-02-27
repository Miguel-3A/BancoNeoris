using BancoNeoris.Api.Movimiento.Negocio;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Movimiento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovimientoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Model.Respuesta<Model.DTO.MovimientoResponse>>> Crear(NuevoMovimiento.MovimientoNuevo data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<Model.Respuesta<List<Model.DTO.MovimientoResponse>>>> Consultar()
        {
            return await _mediator.Send(new ConsultaMovimientos.Ejecuta());
        }

        [HttpGet("{movimientoId}")]
        public async Task<ActionResult<Model.Respuesta<Model.DTO.MovimientoResponse>>> Consultar(string movimientoId)
        {
            return await _mediator.Send(new ConsultaMovimientoId.Ejecuta() { movimientoId = movimientoId });
        }

        [HttpPatch]
        public async Task<ActionResult<Model.Respuesta<Model.DTO.MovimientoResponse>>> Editar(EditarMovimiento.ModificarMovimiento data)
        {
            return await _mediator.Send(data);
        }

        [HttpDelete("{movimientoId}")]
        public async Task<ActionResult<Model.Respuesta<Model.DTO.MovimientoResponse>>> Eliminar(string movimientoId)
        {
            return await _mediator.Send(new EliminarMovimiento.EliminaMovimiento() { movimientoId = movimientoId });
        }

        [HttpGet("{cuentaId}/{fechaInicial}/{fechaFinal}")]
        public async Task<ActionResult<Model.Respuesta<List<Model.DTO.MovimientoResponse>>>> ConsultarFechas(string cuentaId, DateTime fechaInicial, DateTime fechaFinal)
        {
            return await _mediator.Send(new ConsultaFechas.Ejecuta() { cuentaId = cuentaId, fechaInicial = fechaInicial, fechaFinal = fechaFinal });
        }
    }
}
