// <auto-generated />
using System;
using BancoNeoris.Api.Cuenta.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BancoNeoris.Api.Cuenta.Migrations
{
    [DbContext(typeof(ContextoCuenta))]
    [Migration("20230224060943_MigracionSqlServerInicial")]
    partial class MigracionSqlServerInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BancoNeoris.Api.Cuenta.Model.Cuenta", b =>
                {
                    b.Property<Guid?>("cuentaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("clienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("estadoCuenta")
                        .HasColumnType("bit");

                    b.Property<string>("numeroCuenta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("saldoInicial")
                        .HasColumnType("int");

                    b.Property<string>("tipoCuenta")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("cuentaId");

                    b.ToTable("Cuenta");
                });
#pragma warning restore 612, 618
        }
    }
}
