using AutoMapper;
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
    public class ConsultaFechas
    {
        public class Ejecuta : IRequest<Model.Respuesta<List<Model.DTO.MovimientoResponse>>>
        {
            public string cuentaId { get; set; }

            public DateTime fechaInicial { get; set; }

            public DateTime fechaFinal { get; set; }

        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.cuentaId).NotEmpty();
                RuleFor(x => x.fechaInicial).NotEmpty();
                RuleFor(x => x.fechaFinal).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, Model.Respuesta<List<Model.DTO.MovimientoResponse>>>
        {
            private readonly IMovimientoRepository _movimientoRepository;
            private readonly IMapper _mapper;

            public Manejador(IMovimientoRepository movimientoRepository, IMapper mapper)
            {
                _movimientoRepository = movimientoRepository;
                _mapper = mapper;
            }
            public async Task<Model.Respuesta<List<Model.DTO.MovimientoResponse>>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                Model.Respuesta<List<Model.DTO.MovimientoResponse>> respuestaMovimiento = new Model.Respuesta<List<Model.DTO.MovimientoResponse>>() { resultado = false };

                try
                {
                    var movimientos = _movimientoRepository.FiltroMovimientos(request.cuentaId, request.fechaInicial, request.fechaFinal);

                    if (movimientos != null)
                    {
                        respuestaMovimiento.data = _mapper.Map<List<Model.Movimiento>, List<Model.DTO.MovimientoResponse>>(movimientos.ToList());
                        respuestaMovimiento.resultado = true;
                        respuestaMovimiento.mensaje = string.Format("Movimientos con fecha desde {0} hasta fecha {1}, consultados con exito!", request.fechaInicial, request.fechaFinal);
                    }
                    else 
                    {
                        respuestaMovimiento.mensaje = "La cuenta enviada no cuenta con movimientos!";
                    }
                }
                catch (Exception e)
                {

                    respuestaMovimiento.mensaje = e.Message;
                }

                return respuestaMovimiento;
            }
        }
    }
}
