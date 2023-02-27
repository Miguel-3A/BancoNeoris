using BancoNeoris.Api.Cliente.Persistencia;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace BancoNeoris.Api.Cliente.Aplicacion
{
    public class NuevoCliente
    {
        public class ClienteNuevo : IRequest<Model.Respuesta<Model.DTO.ClienteResponse>>
        {
            public string nombre { get; set; }

            public string genero { get; set; }

            public int edad { get; set; }

            public int identificacion { get; set; }

            public string direccion { get; set; }

            public int telefono { get; set; }

            public int contrasena { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<ClienteNuevo>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.nombre).NotEmpty();
                RuleFor(x => x.identificacion).NotEmpty();
                RuleFor(x => x.direccion).NotEmpty();
                RuleFor(x => x.telefono).NotEmpty();
                RuleFor(x => x.contrasena).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<ClienteNuevo, Model.Respuesta<Model.DTO.ClienteResponse>>
        {
            private readonly IClienteRepository _clienteRepository;

            public Manejador(IClienteRepository clienteRepository)
            {
                _clienteRepository = clienteRepository;
            }
            public async Task<Model.Respuesta<Model.DTO.ClienteResponse>> Handle(ClienteNuevo request, CancellationToken cancellationToken)
            {
                Model.Respuesta<Model.DTO.ClienteResponse> respuestaNuevoCliente = new Model.Respuesta<Model.DTO.ClienteResponse>() { resultado = false };

                try
                {
                    var cliente = new Model.Cliente
                    {
                        nombre = request.nombre,
                        genero = request.genero,
                        edad = request.edad,
                        identificacion = request.identificacion,
                        direccion = request.direccion,
                        telefono = request.telefono,
                        contrasena = request.contrasena,
                        estado = true
                    };

                    if (await _clienteRepository.Insert(cliente))
                    {
                        respuestaNuevoCliente.resultado = true;
                        respuestaNuevoCliente.mensaje = "Cliente creado con exito!";
                    }
                    else
                    {
                        respuestaNuevoCliente.mensaje = "No fue posible crear el cliente!";
                    }

                }
                catch (Exception e)
                {

                    respuestaNuevoCliente.mensaje = e.Message;
                }

                return respuestaNuevoCliente;
            }
        }
    }
}
