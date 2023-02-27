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
    public class EditarMovimiento
    {
        public class ModificarMovimiento : IRequest<Model.Respuesta<Model.DTO.MovimientoResponse>>
        {
            public string movimientoId { get; set; }

            public DateTime? fechaMovimiento { get; set; }

            public string tipoMovimiento { get; set; }

            public int valorMovimiento { get; set; }

            public int saldoMovimiento { get; set; }

            public string cuentaId { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<ModificarMovimiento>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.movimientoId).NotEmpty();
                RuleFor(x => x.fechaMovimiento).NotEmpty();
                RuleFor(x => x.tipoMovimiento).NotEmpty();
                RuleFor(x => x.valorMovimiento).NotEmpty();
                RuleFor(x => x.saldoMovimiento).NotEmpty();
                RuleFor(x => x.cuentaId).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<ModificarMovimiento, Model.Respuesta<Model.DTO.MovimientoResponse>>
        {
            private readonly IMovimientoRepository _movimientoRepository;

            public Manejador(IMovimientoRepository movimientoRepository)
            {
                _movimientoRepository = movimientoRepository;
            }

            public async Task<Model.Respuesta<Model.DTO.MovimientoResponse>> Handle(ModificarMovimiento request, CancellationToken cancellationToken)
            {
                Model.Respuesta<Model.DTO.MovimientoResponse> respuestaModificacionMovimiento = new Model.Respuesta<Model.DTO.MovimientoResponse>() { resultado = false };

                try
                {
                    var movimiento = new Model.Movimiento
                    {
                        movimientoId = new Guid(request.movimientoId),
                        fechaMovimiento = request.fechaMovimiento,
                        tipoMovimiento = request.tipoMovimiento,
                        valorMovimiento = request.valorMovimiento,
                        saldoMovimiento = request.saldoMovimiento,
                        cuentaId = new Guid(request.cuentaId)
                    };

                    _movimientoRepository.Update(movimiento);

                    respuestaModificacionMovimiento.resultado = true;
                    respuestaModificacionMovimiento.mensaje = "Movimiento modificado con exito!";
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
