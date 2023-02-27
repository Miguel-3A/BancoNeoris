using AutoMapper;
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
    public class ConsultaClienteId
    {
        public class Ejecuta : IRequest<Model.Respuesta<Model.DTO.ClienteResponse>> {
            public string clienteId { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.clienteId).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, Model.Respuesta<Model.DTO.ClienteResponse>>
        {
            private readonly IClienteRepository _clienteRepository;
            private readonly IMapper _mapper;

            public Manejador(IClienteRepository clienteRepository, IMapper mapper)
            {
                _clienteRepository = clienteRepository;
                _mapper = mapper;
            }
            public async Task<Model.Respuesta<Model.DTO.ClienteResponse>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {              

                Model.Respuesta<Model.DTO.ClienteResponse> respuestaCliente = new Model.Respuesta<Model.DTO.ClienteResponse>() { resultado = false };

                try
                {
                    var cliente = _clienteRepository.GetById(request.clienteId);                    

                    if (cliente != null)
                    {
                        respuestaCliente.data = _mapper.Map<Model.Cliente, Model.DTO.ClienteResponse>(cliente);
                        respuestaCliente.resultado = true;
                        respuestaCliente.mensaje = "Cliente consultado con exito!";
                    }
                    else
                    {
                        respuestaCliente.mensaje = "El cliente no existe!";
                    }

                }
                catch (Exception e)
                {

                    respuestaCliente.mensaje = e.Message;
                }

                

                return respuestaCliente;
            }
        }
    }
}
