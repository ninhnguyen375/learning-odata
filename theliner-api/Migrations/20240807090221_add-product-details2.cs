using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace theliner_api.Migrations
{
    /// <inheritdoc />
    public partial class addproductdetails2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_Products_ProductId",
                table: "ProductDetails");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductDetails",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDetails_ProductId",
                table: "ProductDetails",
                newName: "IX_ProductDetails_ProductID");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "ProductDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Products_ProductID",
                table: "ProductDetails",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_Products_ProductID",
                table: "ProductDetails");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "ProductDetails",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDetails_ProductID",
                table: "ProductDetails",
                newName: "IX_ProductDetails_ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Products_ProductId",
                table: "ProductDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
