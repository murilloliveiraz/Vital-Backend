using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class updatingPacienteModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Alergias",
                table: "pacientes",
                type: "VARCHAR",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistoricoFamiliar",
                table: "pacientes",
                type: "VARCHAR",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medicamentos",
                table: "pacientes",
                type: "VARCHAR",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PCD",
                table: "pacientes",
                type: "Boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alergias",
                table: "pacientes");

            migrationBuilder.DropColumn(
                name: "HistoricoFamiliar",
                table: "pacientes");

            migrationBuilder.DropColumn(
                name: "Medicamentos",
                table: "pacientes");

            migrationBuilder.DropColumn(
                name: "PCD",
                table: "pacientes");

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
        }
    }
}
