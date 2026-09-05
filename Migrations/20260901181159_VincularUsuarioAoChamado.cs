using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskWeb.Migrations
{
    /// <inheritdoc />
    public partial class VincularUsuarioAoChamado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioEmail",
                table: "Chamados",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Chamados",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioEmail",
                table: "Chamados");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Chamados");
        }
    }
}
