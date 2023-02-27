using AutoMapper;
using BancoNeoris.Api.Movimiento.Persistencia;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Movimiento.Negocio
{
    public class ConsultaMovimientos
    {
        public class Ejecuta : IRequest<Model.Respuesta<List<Model.DTO.MovimientoResponse>>> { }

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
                Model.Respuesta<List<Model.DTO.MovimientoResponse>> respuestaMovimientos = new Model.Respuesta<List<Model.DTO.MovimientoResponse>>() { resultado = false };

                try
                {
                    var movimientos = await _movimientoRepository.GetAll();

                    if (movimientos != null && movimientos.Count() > 0)
                    {
                        respuestaMovimientos.data = _mapper.Map<List<Model.Movimiento>, List<Model.DTO.MovimientoResponse>>(movimientos.ToList());
                        respuestaMovimientos.resultado = true;
                        respuestaMovimientos.mensaje = "Movimientos consultados con exito!";
                    }
                    else
                    {
                        respuestaMovimientos.mensaje = "Actualmente no existen movimientos!";
                    }

                }
                catch (Exception e)
                {

                    respuestaMovimientos.mensaje = e.Message;
                }



                return respuestaMovimientos;

            }
        }
    }
}
