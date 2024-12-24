using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_rest_netcore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nombreContacto",
                schema: "colegio",
                table: "Contacts",
                newName: "ContactName");

            migrationBuilder.RenameColumn(
                name: "descripcion",
                schema: "colegio",
                table: "Contacts",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "contacto_id",
                schema: "colegio",
                table: "Contacts",
                newName: "ContactId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "colegio",
                table: "Contacts",
                newName: "descripcion");

            migrationBuilder.RenameColumn(
                name: "ContactName",
                schema: "colegio",
                table: "Contacts",
                newName: "nombreContacto");

            migrationBuilder.RenameColumn(
                name: "ContactId",
                schema: "colegio",
                table: "Contacts",
                newName: "contacto_id");
        }
    }
}
