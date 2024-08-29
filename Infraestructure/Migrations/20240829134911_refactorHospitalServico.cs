using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class refactorHospitalServico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hospitalservico_hospitais_HospitalId",
                table: "hospitalservico");

            migrationBuilder.DropForeignKey(
                name: "FK_hospitalservico_servicos_ServicoId",
                table: "hospitalservico");

            migrationBuilder.AddForeignKey(
                name: "FK_hospitalservico_hospitais_HospitalId",
                table: "hospitalservico",
                column: "HospitalId",
                principalTable: "hospitais",
                principalColumn: "HospitalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_hospitalservico_servicos_ServicoId",
                table: "hospitalservico",
                column: "ServicoId",
                principalTable: "servicos",
                principalColumn: "ServicoId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hospitalservico_hospitais_HospitalId",
                table: "hospitalservico");

            migrationBuilder.DropForeignKey(
                name: "FK_hospitalservico_servicos_ServicoId",
                table: "hospitalservico");

            migrationBuilder.AddForeignKey(
                name: "FK_hospitalservico_hospitais_HospitalId",
                table: "hospitalservico",
                column: "HospitalId",
                principalTable: "hospitais",
                principalColumn: "HospitalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hospitalservico_servicos_ServicoId",
                table: "hospitalservico",
                column: "ServicoId",
                principalTable: "servicos",
                principalColumn: "ServicoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
