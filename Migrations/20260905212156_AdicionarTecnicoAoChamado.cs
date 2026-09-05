using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskWeb.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarTecnicoAoChamado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TecnicoEmail",
                table: "Chamados",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TecnicoId",
                table: "Chamados",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TecnicoEmail",
                table: "Chamados");

            migrationBuilder.DropColumn(
                name: "TecnicoId",
                table: "Chamados");
        }
    }
}
