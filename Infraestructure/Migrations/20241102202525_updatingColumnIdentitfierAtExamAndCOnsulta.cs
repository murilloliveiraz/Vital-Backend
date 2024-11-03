using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class updatingColumnIdentitfierAtExamAndCOnsulta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExameId",
                table: "exames",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ConsultaId",
                table: "consultas",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "QueixasDoPaciente",
                table: "exames",
                type: "VARCHAR",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ObservacoesDaClinica",
                table: "exames",
                type: "VARCHAR",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "exames",
                newName: "ExameId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "consultas",
                newName: "ConsultaId");

            migrationBuilder.AlterColumn<string>(
                name: "QueixasDoPaciente",
                table: "exames",
                type: "VARCHAR",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR");

            migrationBuilder.AlterColumn<string>(
                name: "ObservacoesDaClinica",
                table: "exames",
                type: "VARCHAR",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR");
        }
    }
}
