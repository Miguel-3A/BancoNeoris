using AutoMapper;
using BancoNeoris.Api.Cuenta.Persistencia;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Cuenta.Negocio
{
    public class ConsultaCuentas
    {
        public class Ejecuta : IRequest<Model.Respuesta<List<Model.DTO.CuentaResponse>>> { }

        public class Manejador : IRequestHandler<Ejecuta, Model.Respuesta<List<Model.DTO.CuentaResponse>>>
        {
            private readonly ICuentaRepository _cuentaRepository;
            private readonly IMapper _mapper;

            public Manejador(ICuentaRepository cuentaRepository, IMapper mapper)
            {
                _cuentaRepository = cuentaRepository;
                _mapper = mapper;
            }
            public async Task<Model.Respuesta<List<Model.DTO.CuentaResponse>>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                Model.Respuesta<List<Model.DTO.CuentaResponse>> respuestaCuentas = new Model.Respuesta<List<Model.DTO.CuentaResponse>>() { resultado = false };

                try
                {
                    var cuentas = await _cuentaRepository.GetAll();

                    if (cuentas != null && cuentas.Count() > 0)
                    {
                        respuestaCuentas.data = _mapper.Map<List<Model.Cuenta>, List<Model.DTO.CuentaResponse>>(cuentas.ToList());
                        respuestaCuentas.resultado = true;
                        respuestaCuentas.mensaje = "Cuentas consultadas con exito!";
                    }
                    else
                    {
                        respuestaCuentas.mensaje = "Actualmente no existen cuentas!";
                    }

                }
                catch (Exception e)
                {

                    respuestaCuentas.mensaje = e.Message;
                }



                return respuestaCuentas;

            }
        }
    }
}
