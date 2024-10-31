using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class changingPCDColumnType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PCD",
                table: "pacientes",
                type: "VARCHAR",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "Boolean",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "PCD",
                table: "pacientes",
                type: "Boolean",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldNullable: true);
        }
    }
}
