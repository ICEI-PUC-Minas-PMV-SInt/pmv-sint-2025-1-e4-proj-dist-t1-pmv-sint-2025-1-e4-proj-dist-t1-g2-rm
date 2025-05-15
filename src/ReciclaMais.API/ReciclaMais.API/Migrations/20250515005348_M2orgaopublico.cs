using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReciclaMais.API.Migrations
{
    /// <inheritdoc />
    public partial class M2orgaopublico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Orgao",
                table: "OrgaosPublicos",
                newName: "CNPJ");

            migrationBuilder.RenameColumn(
                name: "cpf",
                table: "Municipes",
                newName: "Cpf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CNPJ",
                table: "OrgaosPublicos",
                newName: "Orgao");

            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "Municipes",
                newName: "cpf");
        }
    }
}
