using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BancoNeoris.Api.Cliente.Migrations
{
    public partial class MigracionSqlServerInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    clienteId = table.Column<Guid>(nullable: false),
                    personaId = table.Column<Guid>(nullable: true),
                    nombre = table.Column<string>(nullable: true),
                    genero = table.Column<string>(nullable: true),
                    edad = table.Column<int>(nullable: false),
                    identificacion = table.Column<int>(nullable: false),
                    direccion = table.Column<string>(nullable: true),
                    telefono = table.Column<int>(nullable: false),
                    contrasena = table.Column<int>(nullable: false),
                    estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.clienteId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
