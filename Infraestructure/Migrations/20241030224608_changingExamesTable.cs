using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class changingExamesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicoId",
                table: "exames",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_exames_MedicoId",
                table: "exames",
                column: "MedicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_exames_medicos_MedicoId",
                table: "exames",
                column: "MedicoId",
                principalTable: "medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_exames_medicos_MedicoId",
                table: "exames");

            migrationBuilder.DropIndex(
                name: "IX_exames_MedicoId",
                table: "exames");

            migrationBuilder.DropColumn(
                name: "MedicoId",
                table: "exames");
        }
    }
}
