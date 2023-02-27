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
    public class EditarCuenta
    {
        public class ModificarCuenta : IRequest<Model.Respuesta<Model.DTO.CuentaResponse>>
        {
            public string cuentaId { get; set; }

            public string numeroCuenta { get; set; }

            public string tipoCuenta { get; set; }

            public int saldoInicial { get; set; }

            public bool estadoCuenta { get; set; }

            public string clienteId { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<ModificarCuenta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.cuentaId).NotEmpty();
                RuleFor(x => x.numeroCuenta).NotEmpty();
                RuleFor(x => x.tipoCuenta).NotEmpty();
                RuleFor(x => x.saldoInicial).NotEmpty();
                RuleFor(x => x.estadoCuenta).NotEmpty();
                RuleFor(x => x.clienteId).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<ModificarCuenta, Model.Respuesta<Model.DTO.CuentaResponse>>
        {
            private readonly ICuentaRepository _cuentaRepository;

            public Manejador(ICuentaRepository cuentaRepository)
            {
                _cuentaRepository = cuentaRepository;
            }

            public async Task<Model.Respuesta<Model.DTO.CuentaResponse>> Handle(ModificarCuenta request, CancellationToken cancellationToken)
            {
                Model.Respuesta<Model.DTO.CuentaResponse> respuestaModificacionCuenta = new Model.Respuesta<Model.DTO.CuentaResponse>() { resultado = false };

                try
                {
                    var cuenta = new Model.Cuenta
                    {
                        cuentaId = new Guid(request.cuentaId),
                        numeroCuenta = request.numeroCuenta,
                        tipoCuenta = request.tipoCuenta,
                        saldoInicial = request.saldoInicial,
                        estadoCuenta = request.estadoCuenta,
                        clienteId = new Guid(request.clienteId)
                    };

                    _cuentaRepository.Update(cuenta);

                    respuestaModificacionCuenta.resultado = true;
                    respuestaModificacionCuenta.mensaje = "Cuenta modificada con exito!";
                }
                catch (Exception e)
                {

                    respuestaModificacionCuenta.mensaje = e.Message;
                }

                return respuestaModificacionCuenta;
            }
        }
    }
}
