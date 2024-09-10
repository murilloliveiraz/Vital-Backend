using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingExamModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "S3KeyPath",
            table: "exames",
            type: "VARCHAR",
            nullable: true,  // Permitir valor nulo
            oldClrType: typeof(string),
            oldType: "VARCHAR");

            migrationBuilder.AlterColumn<string>(
            name: "Base64",
            table: "exames",
            type: "VARCHAR",
            nullable: true,  // Permitir valor nulo
            oldClrType: typeof(string),
            oldType: "VARCHAR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
