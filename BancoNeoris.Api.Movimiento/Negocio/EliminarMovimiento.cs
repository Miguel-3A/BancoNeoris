using BancoNeoris.Api.Movimiento.Persistencia;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Movimiento.Negocio
{
    public class EliminarMovimiento
    {
        public class EliminaMovimiento : IRequest<Model.Respuesta<Model.DTO.MovimientoResponse>>
        {
            public string movimientoId { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<EliminaMovimiento>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.movimientoId).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<EliminaMovimiento, Model.Respuesta<Model.DTO.MovimientoResponse>>
        {
            private readonly IMovimientoRepository _movimientoRepository;

            public Manejador(IMovimientoRepository movimientoRepository)
            {
                _movimientoRepository = movimientoRepository;
            }

            public async Task<Model.Respuesta<Model.DTO.MovimientoResponse>> Handle(EliminaMovimiento request, CancellationToken cancellationToken)
            {
                Model.Respuesta<Model.DTO.MovimientoResponse> respuestaModificacionMovimiento = new Model.Respuesta<Model.DTO.MovimientoResponse>() { resultado = false };

                try
                {

                    _movimientoRepository.DeleteById(request.movimientoId);

                    respuestaModificacionMovimiento.resultado = true;
                    respuestaModificacionMovimiento.mensaje = "Movimiento eliminado con exito!";
                }
                catch (Exception e)
                {

                    respuestaModificacionMovimiento.mensaje = e.Message;
                }

                return respuestaModificacionMovimiento;
            }
        }
    }
}
