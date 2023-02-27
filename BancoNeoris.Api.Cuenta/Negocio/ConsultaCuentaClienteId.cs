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
    public class ConsultaCuentaClienteId
    {
        public class Ejecuta : IRequest<Model.Respuesta<List<Model.DTO.CuentaResponse>>>
        {
            public string clienteId { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.clienteId).NotEmpty();
            }
        }

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

                Model.Respuesta<List<Model.DTO.CuentaResponse>> respuestaCuenta = new Model.Respuesta<List<Model.DTO.CuentaResponse>>() { resultado = false };

                try
                {
                    var cuenta = _cuentaRepository.GetByClienteId(request.clienteId);

                    if (cuenta != null)
                    {
                        respuestaCuenta.data = _mapper.Map<List<Model.Cuenta>, List<Model.DTO.CuentaResponse>>(cuenta.ToList());
                        respuestaCuenta.resultado = true;
                        respuestaCuenta.mensaje = "Cuentas consultadas con exito!";
                    }
                    else
                    {
                        respuestaCuenta.mensaje = "El cliente no tiene cuentas!";
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
