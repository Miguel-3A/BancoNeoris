using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BancoNeoris.Api.Movimiento.Migrations
{
    public partial class MigracionSqlServerInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movimiento",
                columns: table => new
                {
                    movimientoId = table.Column<Guid>(nullable: false),
                    fechaMovimiento = table.Column<DateTime>(nullable: true),
                    tipoMovimiento = table.Column<string>(nullable: true),
                    valorMovimiento = table.Column<int>(nullable: false),
                    saldoMovimiento = table.Column<int>(nullable: false),
                    cuentaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimiento", x => x.movimientoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimiento");
        }
    }
}
