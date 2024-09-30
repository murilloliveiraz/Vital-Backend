using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class creatingConsulta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "S3KeyPath",
                table: "exames",
                type: "VARCHAR",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR");

            migrationBuilder.AlterColumn<string>(
                name: "Base64",
                table: "exames",
                type: "VARCHAR",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR");

            migrationBuilder.CreateTable(
                name: "Consulta",
                columns: table => new
                {
                    ConsultaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ValorConsulta = table.Column<decimal>(type: "numeric", nullable: false),
                    StatusPagamento = table.Column<string>(type: "text", nullable: false),
                    TipoConsulta = table.Column<string>(type: "text", nullable: false),
                    Local = table.Column<string>(type: "text", nullable: true),
                    MeetLink = table.Column<string>(type: "text", nullable: true),
                    Data = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PacienteId = table.Column<int>(type: "integer", nullable: false),
                    MedicoId = table.Column<int>(type: "integer", nullable: false),
                    S3KeyPathAtestado = table.Column<string>(type: "text", nullable: true),
                    S3KeyPathReceita = table.Column<string>(type: "text", nullable: true),
                    PrefixoDaPasta = table.Column<string>(type: "text", nullable: false),
                    AtestadoBase64 = table.Column<string>(type: "text", nullable: true),
                    ReceitaBase64 = table.Column<string>(type: "text", nullable: true),
                    EmailParaReceberNotificacoes = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consulta", x => x.ConsultaId);
                    table.ForeignKey(
                        name: "FK_Consulta_medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consulta_pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_MedicoId",
                table: "Consulta",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_PacienteId",
                table: "Consulta",
                column: "PacienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consulta");

            migrationBuilder.AlterColumn<string>(
                name: "S3KeyPath",
                table: "exames",
                type: "VARCHAR",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Base64",
                table: "exames",
                type: "VARCHAR",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldNullable: true);
        }
    }
}
