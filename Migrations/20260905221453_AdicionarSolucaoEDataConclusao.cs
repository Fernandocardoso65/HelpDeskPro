using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskWeb.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarSolucaoEDataConclusao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataConclusao",
                table: "Chamados",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Solucao",
                table: "Chamados",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataConclusao",
                table: "Chamados");

            migrationBuilder.DropColumn(
                name: "Solucao",
                table: "Chamados");
        }
    }
}
