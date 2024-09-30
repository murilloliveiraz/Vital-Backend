using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedConsulta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consulta_medicos_MedicoId",
                table: "Consulta");

            migrationBuilder.DropForeignKey(
                name: "FK_Consulta_pacientes_PacienteId",
                table: "Consulta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Consulta",
                table: "Consulta");

            migrationBuilder.RenameTable(
                name: "Consulta",
                newName: "consultas");

            migrationBuilder.RenameIndex(
                name: "IX_Consulta_PacienteId",
                table: "consultas",
                newName: "IX_consultas_PacienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Consulta_MedicoId",
                table: "consultas",
                newName: "IX_consultas_MedicoId");

            migrationBuilder.AlterColumn<string>(
                name: "ValorConsulta",
                table: "consultas",
                type: "VARCHAR",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TipoConsulta",
                table: "consultas",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "StatusPagamento",
                table: "consultas",
                type: "VARCHAR",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "consultas",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "S3KeyPathReceita",
                table: "consultas",
                type: "VARCHAR",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "S3KeyPathAtestado",
                table: "consultas",
                type: "VARCHAR",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceitaBase64",
                table: "consultas",
                type: "VARCHAR",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrefixoDaPasta",
                table: "consultas",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "consultas",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "MeetLink",
                table: "consultas",
                type: "VARCHAR",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Local",
                table: "consultas",
                type: "VARCHAR",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailParaReceberNotificacoes",
                table: "consultas",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "consultas",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "AtestadoBase64",
                table: "consultas",
                type: "VARCHAR",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_consultas",
                table: "consultas",
                column: "ConsultaId");

            migrationBuilder.AddForeignKey(
                name: "FK_consultas_medicos_MedicoId",
                table: "consultas",
                column: "MedicoId",
                principalTable: "medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_consultas_pacientes_PacienteId",
                table: "consultas",
                column: "PacienteId",
                principalTable: "pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consultas_medicos_MedicoId",
                table: "consultas");

            migrationBuilder.DropForeignKey(
                name: "FK_consultas_pacientes_PacienteId",
                table: "consultas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_consultas",
                table: "consultas");

            migrationBuilder.RenameTable(
                name: "consultas",
                newName: "Consulta");

            migrationBuilder.RenameIndex(
                name: "IX_consultas_PacienteId",
                table: "Consulta",
                newName: "IX_Consulta_PacienteId");

            migrationBuilder.RenameIndex(
                name: "IX_consultas_MedicoId",
                table: "Consulta",
                newName: "IX_Consulta_MedicoId");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorConsulta",
                table: "Consulta",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TipoConsulta",
                table: "Consulta",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");

            migrationBuilder.AlterColumn<string>(
                name: "StatusPagamento",
                table: "Consulta",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Consulta",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");

            migrationBuilder.AlterColumn<string>(
                name: "S3KeyPathReceita",
                table: "Consulta",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "S3KeyPathAtestado",
                table: "Consulta",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceitaBase64",
                table: "Consulta",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrefixoDaPasta",
                table: "Consulta",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Consulta",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");

            migrationBuilder.AlterColumn<string>(
                name: "MeetLink",
                table: "Consulta",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Local",
                table: "Consulta",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR");

            migrationBuilder.AlterColumn<string>(
                name: "EmailParaReceberNotificacoes",
                table: "Consulta",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "Consulta",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<string>(
                name: "AtestadoBase64",
                table: "Consulta",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consulta",
                table: "Consulta",
                column: "ConsultaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consulta_medicos_MedicoId",
                table: "Consulta",
                column: "MedicoId",
                principalTable: "medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consulta_pacientes_PacienteId",
                table: "Consulta",
                column: "PacienteId",
                principalTable: "pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
