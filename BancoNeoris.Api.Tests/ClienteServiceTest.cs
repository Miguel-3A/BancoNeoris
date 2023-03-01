using AutoMapper;
using BancoNeoris.Api.Cliente.Aplicacion;
using BancoNeoris.Api.Cliente.Negocio;
using BancoNeoris.Api.Cliente.Persistencia;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BancoNeoris.Api.Tests
{
    public class ClienteServiceTest
    {
        private IEnumerable<Cliente.Model.Cliente> ObtenerDataPrueba()
        {
            A.Configure<Cliente.Model.Cliente>()
                .Fill(x => x.clienteId, () => { return Guid.NewGuid(); })
                .Fill(x => x.nombre).AsFirstName()
                .Fill(x => x.genero).AsPersonTitle()
                .Fill(x => x.edad).WithinRange(1, 99)
                .Fill(x => x.identificacion).WithinRange(000000001, 999999999)
                .Fill(x => x.direccion).AsAddress()
                .Fill(x => x.telefono).WithinRange(000000001, 999999999)
                .Fill(x => x.contrasena).WithinRange(0000, 9999)
                .Fill(x => x.estado, () => { return true; });

            var lista = A.ListOf<Cliente.Model.Cliente>(30);

            lista[0].identificacion = 123456;

            return lista;
        }

        private Mock<ContextoCliente> CrearContexto()
        {
            var dataPrueba = ObtenerDataPrueba().AsQueryable();

            var dbSet = new Mock<DbSet<Cliente.Model.Cliente>>();
            dbSet.As<IQueryable<Cliente.Model.Cliente>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<Cliente.Model.Cliente>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<Cliente.Model.Cliente>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<Cliente.Model.Cliente>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

            dbSet.As<IAsyncEnumerable<Cliente.Model.Cliente>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<Cliente.Model.Cliente>(dataPrueba.GetEnumerator()));

            dbSet.As<IQueryable<Cliente.Model.Cliente>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<Cliente.Model.Cliente>(dataPrueba.Provider));

            var contexto = new Mock<ContextoCliente>();
            contexto.Setup(x => x.Cliente).Returns(dbSet.Object);

            return contexto;
        }

        [Fact]
        public async void GetClienteIdentificacion()
        {
            //System.Diagnostics.Debugger.Launch();

            var mockICLiente = new ClienteRepository(CrearContexto().Object);
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });
            var mapper = mapConfig.CreateMapper();

            var request = new ConsultaIdentificacionCliente.ConsultaCliente();
            request.clienteIdentificacion = 123456;

            ConsultaIdentificacionCliente.Manejador manejador = new ConsultaIdentificacionCliente.Manejador(mockICLiente, mapper);
            var cliente = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(cliente.data);
            Assert.True(cliente.data.identificacion == 123456);
        }

        [Fact]
        public async void GetClientes()
        {
            //System.Diagnostics.Debugger.Launch();

            var mockICLiente = new ClienteRepository(CrearContexto().Object);
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });
            var mapper = mapConfig.CreateMapper();

            ConsultaClientes.Manejador manejador = new ConsultaClientes.Manejador(mockICLiente, mapper);
            ConsultaClientes.Ejecuta request = new ConsultaClientes.Ejecuta();

            var lista = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(lista.data.Any());
        }

        [Fact]
        public async void GuardaCliente()
        {
            //System.Diagnostics.Debugger.Launch();

            var options = new DbContextOptionsBuilder<ContextoCliente>()
                    .UseInMemoryDatabase(databaseName: "BaseCliente")
                    .Options;

            var contexto = new ContextoCliente(options);
            var mockICLiente = new ClienteRepository(contexto);

            var request = new NuevoCliente.ClienteNuevo();
            request.nombre = "Pepito";
            request.genero = "Masculino";
            request.edad = 30;
            request.identificacion = 147852;
            request.direccion = "Calle 12";
            request.telefono = 9876543;
            request.contrasena = 9876;

            NuevoCliente.Manejador manejador = new NuevoCliente.Manejador(mockICLiente);

            var cliente = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(cliente != null);
        }
    }
}
