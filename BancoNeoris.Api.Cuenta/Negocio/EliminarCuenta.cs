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
    public class EliminarCuenta
    {
        public class EliminaCuenta : IRequest<Model.Respuesta<Model.DTO.CuentaResponse>>
        {
            public string cuentaId { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<EliminaCuenta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.cuentaId).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<EliminaCuenta, Model.Respuesta<Model.DTO.CuentaResponse>>
        {
            private readonly ICuentaRepository _cuentaRepository;

            public Manejador(ICuentaRepository cuentaRepository)
            {
                _cuentaRepository = cuentaRepository;
            }

            public async Task<Model.Respuesta<Model.DTO.CuentaResponse>> Handle(EliminaCuenta request, CancellationToken cancellationToken)
            {
                Model.Respuesta<Model.DTO.CuentaResponse> respuestaModificacionCuenta = new Model.Respuesta<Model.DTO.CuentaResponse>() { resultado = false };

                try
                {

                    _cuentaRepository.DeleteById(request.cuentaId);

                    respuestaModificacionCuenta.resultado = true;
                    respuestaModificacionCuenta.mensaje = "Cuenta eliminada con exito!";
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
