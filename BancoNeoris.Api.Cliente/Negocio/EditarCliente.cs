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
    public class EditarCliente
    {
        public class ModificarCliente : IRequest<Model.Respuesta<Model.DTO.ClienteResponse>>
        {
            public string clienteId { get; set; }

            public string nombre { get; set; }

            public string genero { get; set; }

            public int edad { get; set; }

            public string direccion { get; set; }

            public int telefono { get; set; }

            public int contrasena { get; set; }

            public bool estado { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<ModificarCliente>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.clienteId).NotEmpty();
                RuleFor(x => x.nombre).NotEmpty();
                RuleFor(x => x.direccion).NotEmpty();
                RuleFor(x => x.telefono).NotEmpty();
                RuleFor(x => x.contrasena).NotEmpty();
                RuleFor(x => x.estado).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<ModificarCliente, Model.Respuesta<Model.DTO.ClienteResponse>>
        {
            private readonly IClienteRepository _clienteRepository;

            public Manejador(IClienteRepository clienteRepository)
            {
                _clienteRepository = clienteRepository;
            }

            public async Task<Model.Respuesta<Model.DTO.ClienteResponse>> Handle(ModificarCliente request, CancellationToken cancellationToken)
            {
                Model.Respuesta<Model.DTO.ClienteResponse> respuestaModificacionCliente = new Model.Respuesta<Model.DTO.ClienteResponse>() { resultado = false };

                try
                {
                    var cliente = new Model.Cliente
                    {
                        clienteId = new Guid(request.clienteId),
                        nombre = request.nombre,
                        genero = request.genero,
                        edad = request.edad,
                        direccion = request.direccion,
                        telefono = request.telefono,
                        contrasena = request.contrasena,
                        estado = request.estado
                    };

                    _clienteRepository.Update(cliente);

                    respuestaModificacionCliente.resultado = true;
                    respuestaModificacionCliente.mensaje = "Cliente modificado con exito!";
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
