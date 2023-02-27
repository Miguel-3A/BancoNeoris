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
    public class NuevaCuenta
    {
        public class CuentaNueva : IRequest<Model.Respuesta<Model.DTO.CuentaResponse>>
        {
            public string numeroCuenta { get; set; }

            public string tipoCuenta { get; set; }

            public int saldoInicial { get; set; }

            public string clienteId { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<CuentaNueva>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.numeroCuenta).NotEmpty();
                RuleFor(x => x.tipoCuenta).NotEmpty();
                RuleFor(x => x.saldoInicial).NotEmpty();
                RuleFor(x => x.clienteId).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<CuentaNueva, Model.Respuesta<Model.DTO.CuentaResponse>>
        {
            private readonly ICuentaRepository _cuentaRepository;

            public Manejador(ICuentaRepository cuentaRepository)
            {
                _cuentaRepository = cuentaRepository;
            }
            public async Task<Model.Respuesta<Model.DTO.CuentaResponse>> Handle(CuentaNueva request, CancellationToken cancellationToken)
            {
                Model.Respuesta<Model.DTO.CuentaResponse> respuestaNuevoCuenta = new Model.Respuesta<Model.DTO.CuentaResponse>() { resultado = false };

                try
                {
                    var cuenta = new Model.Cuenta
                    {
                        numeroCuenta = request.numeroCuenta,
                        tipoCuenta = request.tipoCuenta,
                        saldoInicial = request.saldoInicial,
                        clienteId = new Guid(request.clienteId),
                        estadoCuenta = true
                    };

                    if (await _cuentaRepository.Insert(cuenta))
                    {
                        respuestaNuevoCuenta.resultado = true;
                        respuestaNuevoCuenta.mensaje = "Cuenta creada con exito!";
                    }
                    else
                    {
                        respuestaNuevoCuenta.mensaje = "No fue posible crear la cuenta!";
                    }

                }
                catch (Exception e)
                {

                    respuestaNuevoCuenta.mensaje = e.Message;
                }

                return respuestaNuevoCuenta;
            }
        }
    }
}
