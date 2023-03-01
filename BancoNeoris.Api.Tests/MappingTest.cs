using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BancoNeoris.Api.Tests
{
    public class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<Cliente.Model.Cliente, Cliente.Model.DTO.ClienteResponse>();

            CreateMap<Cuenta.Model.Cuenta, Cuenta.Model.DTO.CuentaResponse>();
        }
    }
}
