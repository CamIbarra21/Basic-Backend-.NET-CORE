using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prueba_ProductsEF.Migrations
{
    /// <inheritdoc />
    public partial class RemoveHasStockFromProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasStock",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasStock",
                table: "Products",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
