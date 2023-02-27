using BancoNeoris.Api.Cliente.Persistencia;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Cliente.Negocio
{
    public class EliminarCliente
    {
        public class EliminaCliente : IRequest<Model.Respuesta<Model.DTO.ClienteResponse>>
        {
            public string clienteId { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<EliminaCliente>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.clienteId).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<EliminaCliente, Model.Respuesta<Model.DTO.ClienteResponse>>
        {
            private readonly IClienteRepository _clienteRepository;

            public Manejador(IClienteRepository clienteRepository)
            {
                _clienteRepository = clienteRepository;
            }

            public async Task<Model.Respuesta<Model.DTO.ClienteResponse>> Handle(EliminaCliente request, CancellationToken cancellationToken)
            {
                Model.Respuesta<Model.DTO.ClienteResponse> respuestaModificacionCliente = new Model.Respuesta<Model.DTO.ClienteResponse>() { resultado = false };

                try
                {

                    _clienteRepository.DeleteById(request.clienteId);

                    respuestaModificacionCliente.resultado = true;
                    respuestaModificacionCliente.mensaje = "Cliente eliminado con exito!";
                }
                catch (Exception e)
                {

                    respuestaModificacionCliente.mensaje = e.Message;
                }

                return respuestaModificacionCliente;
            }
        }
    }
}
