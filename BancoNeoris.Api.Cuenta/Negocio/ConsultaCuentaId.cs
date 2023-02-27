using AutoMapper;
using BancoNeoris.Api.Cuenta.Persistencia;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Cuenta.Negocio
{
    public class ConsultaCuentaId
    {
        public class Ejecuta : IRequest<Model.Respuesta<Model.DTO.CuentaResponse>>
        {
            public string cuentaId { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.cuentaId).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, Model.Respuesta<Model.DTO.CuentaResponse>>
        {
            private readonly ICuentaRepository _cuentaRepository;
            private readonly IMapper _mapper;

            public Manejador(ICuentaRepository cuentaRepository, IMapper mapper)
            {
                _cuentaRepository = cuentaRepository;
                _mapper = mapper;
            }
            public async Task<Model.Respuesta<Model.DTO.CuentaResponse>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                Model.Respuesta<Model.DTO.CuentaResponse> respuestaCuenta = new Model.Respuesta<Model.DTO.CuentaResponse>() { resultado = false };

                try
                {
                    var cuenta = _cuentaRepository.GetById(request.cuentaId);

                    if (cuenta != null)
                    {
                        respuestaCuenta.data = _mapper.Map<Model.Cuenta, Model.DTO.CuentaResponse>(cuenta);
                        respuestaCuenta.resultado = true;
                        respuestaCuenta.mensaje = "Cuenta consultada con exito!";
                    }
                    else
                    {
                        respuestaCuenta.mensaje = "La cuenta no existe!";
                    }

                }
                catch (Exception e)
                {

                    respuestaCuenta.mensaje = e.Message;
                }



                return respuestaCuenta;
            }
        }
    }
}
