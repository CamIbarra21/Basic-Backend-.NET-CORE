using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prueba_ProductsEF.Migrations
{
    /// <inheritdoc />
    public partial class RecreateUserRolTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "temp",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "temp",
                table: "Rols",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Rols",
                keyColumn: "Id",
                keyValue: 1,
                column: "temp",
                value: "xd");

            migrationBuilder.UpdateData(
                table: "Rols",
                keyColumn: "Id",
                keyValue: 2,
                column: "temp",
                value: "xd");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "temp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "temp",
                table: "Rols");
        }
    }
}
