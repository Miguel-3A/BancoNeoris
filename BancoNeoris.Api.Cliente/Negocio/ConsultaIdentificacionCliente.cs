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
    public class ConsultaIdentificacionCliente
    {
        public class ConsultaCliente : IRequest<Model.Respuesta<Model.DTO.ClienteResponse>>
        {
            public int clienteIdentificacion { get; set; }

            public string fechaInicial { get; set; }

            public string fechaFinal { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<ConsultaCliente>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.clienteIdentificacion).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<ConsultaCliente, Model.Respuesta<Model.DTO.ClienteResponse>>
        {
            private readonly IClienteRepository _clienteRepository;
            private readonly IMapper _mapper;

            public Manejador(IClienteRepository clienteRepository, IMapper mapper)
            {
                _clienteRepository = clienteRepository;
                _mapper = mapper;
            }
            public async Task<Model.Respuesta<Model.DTO.ClienteResponse>> Handle(ConsultaCliente request, CancellationToken cancellationToken)
            {

                Model.Respuesta<Model.DTO.ClienteResponse> respuestaCliente = new Model.Respuesta<Model.DTO.ClienteResponse>() { resultado = false };

                try
                {
                    var cliente = _clienteRepository.GetByIdentificacion(request.clienteIdentificacion);

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
