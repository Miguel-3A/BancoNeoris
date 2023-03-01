using AutoMapper;
using BancoNeoris.Api.Cuenta.Negocio;
using BancoNeoris.Api.Cuenta.Persistencia;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BancoNeoris.Api.Tests
{
    public class CuentaServiceTest
    {
        private IEnumerable<Cuenta.Model.Cuenta> ObtenerDataPrueba()
        {
            A.Configure<Cuenta.Model.Cuenta>()
                .Fill(x => x.cuentaId, () => { return Guid.NewGuid(); })
                .Fill(x => x.numeroCuenta).AsPhoneNumber()
                .Fill(x => x.tipoCuenta).AsAddress()
                .Fill(x => x.saldoInicial).WithinRange(1, 999999999)
                .Fill(x => x.estadoCuenta, () => { return true; })
                .Fill(x => x.clienteId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<Cuenta.Model.Cuenta>(30);

            return lista;
        }

        private Mock<ContextoCuenta> CrearContexto()
        {
            var dataPrueba = ObtenerDataPrueba().AsQueryable();

            var dbSet = new Mock<DbSet<Cuenta.Model.Cuenta>>();
            dbSet.As<IQueryable<Cuenta.Model.Cuenta>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<Cuenta.Model.Cuenta>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<Cuenta.Model.Cuenta>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<Cuenta.Model.Cuenta>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

            dbSet.As<IAsyncEnumerable<Cuenta.Model.Cuenta>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<Cuenta.Model.Cuenta>(dataPrueba.GetEnumerator()));

            dbSet.As<IQueryable<Cuenta.Model.Cuenta>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<Cuenta.Model.Cuenta>(dataPrueba.Provider));

            var contexto = new Mock<ContextoCuenta>();
            contexto.Setup(x => x.Cuenta).Returns(dbSet.Object);

            return contexto;
        }


        [Fact]
        public async void GetCuentas()
        {
            //System.Diagnostics.Debugger.Launch();

            var mockICuenta = new CuentaRepository(CrearContexto().Object);
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });
            var mapper = mapConfig.CreateMapper();

            ConsultaCuentas.Manejador manejador = new ConsultaCuentas.Manejador(mockICuenta, mapper);
            ConsultaCuentas.Ejecuta request = new ConsultaCuentas.Ejecuta();

            var lista = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(lista.data.Any());
        }

        [Fact]
        public async void GuardaCuenta()
        {
            //System.Diagnostics.Debugger.Launch();

            var options = new DbContextOptionsBuilder<ContextoCuenta>()
                    .UseInMemoryDatabase(databaseName: "BaseCuenta")
                    .Options;

            var contexto = new ContextoCuenta(options);
            var mockICuenta = new CuentaRepository(contexto);

            var request = new NuevaCuenta.CuentaNueva();
            request.numeroCuenta = "123456789";
            request.tipoCuenta = "Ahorros";
            request.saldoInicial = 30;
            request.clienteId = Guid.NewGuid().ToString();


            NuevaCuenta.Manejador manejador = new NuevaCuenta.Manejador(mockICuenta);

            var Cuenta = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(Cuenta != null);
        }
    }
}
