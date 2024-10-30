using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class updatingProntuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64",
                table: "exames");

            migrationBuilder.DropColumn(
                name: "AtestadoBase64",
                table: "consultas");

            migrationBuilder.DropColumn(
                name: "ReceitaBase64",
                table: "consultas");

            migrationBuilder.DropColumn(
                name: "S3KeyPathAtestado",
                table: "consultas");

            migrationBuilder.DropColumn(
                name: "S3KeyPathReceita",
                table: "consultas");

            migrationBuilder.AddColumn<string>(
                name: "Alergias",
                table: "Prontuarios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistoricoFamiliar",
                table: "Prontuarios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medicamentos",
                table: "Prontuarios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PCD",
                table: "Prontuarios",
                type: "boolean",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "documentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConsultaId = table.Column<int>(type: "integer", nullable: false),
                    S3KeyPath = table.Column<string>(type: "VARCHAR", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_documentos_consultas_ConsultaId",
                        column: x => x.ConsultaId,
                        principalTable: "consultas",
                        principalColumn: "ConsultaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_documentos_ConsultaId",
                table: "documentos",
                column: "ConsultaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "documentos");

            migrationBuilder.DropColumn(
                name: "Alergias",
                table: "Prontuarios");

            migrationBuilder.DropColumn(
                name: "HistoricoFamiliar",
                table: "Prontuarios");

            migrationBuilder.DropColumn(
                name: "Medicamentos",
                table: "Prontuarios");

            migrationBuilder.DropColumn(
                name: "PCD",
                table: "Prontuarios");

            migrationBuilder.AddColumn<string>(
                name: "Base64",
                table: "exames",
                type: "VARCHAR",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AtestadoBase64",
                table: "consultas",
                type: "VARCHAR",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceitaBase64",
                table: "consultas",
                type: "VARCHAR",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "S3KeyPathAtestado",
                table: "consultas",
                type: "VARCHAR",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "S3KeyPathReceita",
                table: "consultas",
                type: "VARCHAR",
                nullable: true);
        }
    }
}
