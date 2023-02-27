using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BancoNeoris.Api.Cuenta.Migrations
{
    public partial class MigracionSqlServerInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cuenta",
                columns: table => new
                {
                    cuentaId = table.Column<Guid>(nullable: false),
                    numeroCuenta = table.Column<string>(nullable: true),
                    tipoCuenta = table.Column<string>(nullable: true),
                    saldoInicial = table.Column<int>(nullable: false),
                    estadoCuenta = table.Column<bool>(nullable: false),
                    clienteId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuenta", x => x.cuentaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cuenta");
        }
    }
}
