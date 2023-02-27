using AutoMapper;
using BancoNeoris.Api.Cliente.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Cliente.Negocio
{
    public class ConsultaClientes
    {
        public class Ejecuta : IRequest<Model.Respuesta<List<Model.DTO.ClienteResponse>>> { }

        public class Manejador : IRequestHandler<Ejecuta, Model.Respuesta<List<Model.DTO.ClienteResponse>>>
        {
            private readonly IClienteRepository _clienteRepository;
            private readonly IMapper _mapper; 

            public Manejador(IClienteRepository clienteRepository, IMapper mapper)
            {
                _clienteRepository = clienteRepository;
                _mapper = mapper;
            }
            public async Task<Model.Respuesta<List<Model.DTO.ClienteResponse>>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                Model.Respuesta<List<Model.DTO.ClienteResponse>> respuestaClientes = new Model.Respuesta<List<Model.DTO.ClienteResponse>>() { resultado = false };

                try
                {
                    var clientes = await _clienteRepository.GetAll();

                    if (clientes != null && clientes.Count() > 0)
                    {
                        respuestaClientes.data = _mapper.Map<List<Model.Cliente>, List<Model.DTO.ClienteResponse>>(clientes.ToList());
                        respuestaClientes.resultado = true;
                        respuestaClientes.mensaje = "Clientes consultados con exito!";
                    }
                    else
                    {
                        respuestaClientes.mensaje = "Actualmente no existen clientes!";
                    }

                }
                catch (Exception e)
                {

                    respuestaClientes.mensaje = e.Message;
                }



                return respuestaClientes;

            }
        }
    }
}
