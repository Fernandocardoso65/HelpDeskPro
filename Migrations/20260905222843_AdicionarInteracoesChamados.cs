using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskWeb.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarInteracoesChamados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InteracoesChamados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChamadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Mensagem = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<string>(type: "TEXT", nullable: true),
                    UsuarioEmail = table.Column<string>(type: "TEXT", nullable: true),
                    Perfil = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteracoesChamados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InteracoesChamados_Chamados_ChamadoId",
                        column: x => x.ChamadoId,
                        principalTable: "Chamados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InteracoesChamados_ChamadoId",
                table: "InteracoesChamados",
                column: "ChamadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InteracoesChamados");
        }
    }
}
