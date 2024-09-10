using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ExamModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CaminhoResultado",
                table: "exames",
                newName: "S3KeyPath");

            migrationBuilder.RenameColumn(
                name: "ArquivoResultadoUrl",
                table: "exames",
                newName: "PrefixoDaPasta");

            migrationBuilder.AddColumn<string>(
                name: "Base64",
                table: "exames",
                type: "VARCHAR",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "exames",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64",
                table: "exames");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "exames");

            migrationBuilder.RenameColumn(
                name: "S3KeyPath",
                table: "exames",
                newName: "CaminhoResultado");

            migrationBuilder.RenameColumn(
                name: "PrefixoDaPasta",
                table: "exames",
                newName: "ArquivoResultadoUrl");
        }
    }
}
