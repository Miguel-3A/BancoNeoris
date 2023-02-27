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
    public class ConsultaMovimientoId
    {
        public class Ejecuta : IRequest<Model.Respuesta<Model.DTO.MovimientoResponse>>
        {
            public string movimientoId { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.movimientoId).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, Model.Respuesta<Model.DTO.MovimientoResponse>>
        {
            private readonly IMovimientoRepository _movimientoRepository;
            private readonly IMapper _mapper;

            public Manejador(IMovimientoRepository movimientoRepository, IMapper mapper)
            {
                _movimientoRepository = movimientoRepository;
                _mapper = mapper;
            }
            public async Task<Model.Respuesta<Model.DTO.MovimientoResponse>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                Model.Respuesta<Model.DTO.MovimientoResponse> respuestaMovimiento = new Model.Respuesta<Model.DTO.MovimientoResponse>() { resultado = false };

                try
                {
                    var movimiento = _movimientoRepository.GetById(request.movimientoId);

                    if (movimiento != null)
                    {
                        respuestaMovimiento.data = _mapper.Map<Model.Movimiento, Model.DTO.MovimientoResponse>(movimiento);
                        respuestaMovimiento.resultado = true;
                        respuestaMovimiento.mensaje = "Movimiento consultado con exito!";
                    }
                    else
                    {
                        respuestaMovimiento.mensaje = "El movimiento no existe!";
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
