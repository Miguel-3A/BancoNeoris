﻿// <auto-generated />
using System;
using BancoNeoris.Api.Movimiento.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BancoNeoris.Api.Movimiento.Migrations
{
    [DbContext(typeof(ContextoMovimiento))]
    [Migration("20230224064143_MigracionSqlServerInicial")]
    partial class MigracionSqlServerInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BancoNeoris.Api.Movimiento.Model.Movimiento", b =>
                {
                    b.Property<Guid?>("movimientoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("cuentaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("fechaMovimiento")
                        .HasColumnType("datetime2");

                    b.Property<int>("saldoMovimiento")
                        .HasColumnType("int");

                    b.Property<string>("tipoMovimiento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("valorMovimiento")
                        .HasColumnType("int");

                    b.HasKey("movimientoId");

                    b.ToTable("Movimiento");
                });
#pragma warning restore 612, 618
        }
    }
}