using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_rest_netcore.Migrations
{
    /// <inheritdoc />
    public partial class CreandoBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "colegio");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "colegio");

            migrationBuilder.RenameColumn(
                name: "passwordHash",
                schema: "colegio",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "email",
                schema: "colegio",
                table: "Users",
                newName: "Email");

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "colegio",
                columns: table => new
                {
                    contacto_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreContacto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.contacto_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "colegio");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "colegio",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "passwordHash");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "email");
        }
    }
}
