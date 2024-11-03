using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class updatingConsultaAndServicoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Especializacao",
                table: "servicos",
                type: "VARCHAR",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QueixasDoPaciente",
                table: "consultas",
                type: "VARCHAR",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Especializacao",
                table: "servicos");

            migrationBuilder.DropColumn(
                name: "QueixasDoPaciente",
                table: "consultas");
        }
    }
}
