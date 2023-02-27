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
    public class NuevoMovimiento
    {
        public class MovimientoNuevo : IRequest<Model.Respuesta<Model.DTO.MovimientoResponse>>
        {
            public DateTime? fechaMovimiento { get; set; }

            public string tipoMovimiento { get; set; }

            public int valorMovimiento { get; set; }

            public int saldoMovimiento { get; set; }

            public string cuentaId { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<MovimientoNuevo>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.fechaMovimiento).NotEmpty();                
                RuleFor(x => x.tipoMovimiento).NotEmpty().Must(x => x.ToUpper().Equals("DEPOSITO") || x.ToUpper().Equals("RETIRO")).WithMessage("El tipo de movimiento no existe");
                RuleFor(x => x.valorMovimiento).NotEmpty();
                RuleFor(x => x.saldoMovimiento).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Saldo no disponible");
                RuleFor(x => x.cuentaId).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<MovimientoNuevo, Model.Respuesta<Model.DTO.MovimientoResponse>>
        {
            private readonly IMovimientoRepository _movimientoRepository;

            public Manejador(IMovimientoRepository movimientoRepository)
            {
                _movimientoRepository = movimientoRepository;
            }
            public async Task<Model.Respuesta<Model.DTO.MovimientoResponse>> Handle(MovimientoNuevo request, CancellationToken cancellationToken)
            {
                Model.Respuesta<Model.DTO.MovimientoResponse> respuestaNuevoMovimiento = new Model.Respuesta<Model.DTO.MovimientoResponse>() { resultado = false };

                try
                {
                    var movimiento = new Model.Movimiento
                    {
                        fechaMovimiento = request.fechaMovimiento,
                        tipoMovimiento = request.tipoMovimiento,
                        valorMovimiento = request.valorMovimiento,
                        saldoMovimiento = request.saldoMovimiento,
                        cuentaId = new Guid(request.cuentaId)
                    };

                    if (await _movimientoRepository.Insert(movimiento))
                    {
                        respuestaNuevoMovimiento.resultado = true;
                        respuestaNuevoMovimiento.mensaje = "Movimiento creado con exito!";
                    }
                    else
                    {
                        respuestaNuevoMovimiento.mensaje = "No fue posible crear el movimiento!";
                    }

                }
                catch (Exception e)
                {

                    respuestaNuevoMovimiento.mensaje = e.Message;
                }

                return respuestaNuevoMovimiento;
            }
        }
    }
}
