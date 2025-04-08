using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApp.Migrations
{
    /// <inheritdoc />
    public partial class ProductAndRelatedModelsSome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSize_products_ProductsId",
                table: "ProductSize");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSize_sizes_SizesId",
                table: "ProductSize");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSize",
                table: "ProductSize");

            migrationBuilder.RenameTable(
                name: "ProductSize",
                newName: "product_to_size");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSize_SizesId",
                table: "product_to_size",
                newName: "IX_product_to_size_SizesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_to_size",
                table: "product_to_size",
                columns: new[] { "ProductsId", "SizesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_product_to_size_products_ProductsId",
                table: "product_to_size",
                column: "ProductsId",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_to_size_sizes_SizesId",
                table: "product_to_size",
                column: "SizesId",
                principalTable: "sizes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_to_size_products_ProductsId",
                table: "product_to_size");

            migrationBuilder.DropForeignKey(
                name: "FK_product_to_size_sizes_SizesId",
                table: "product_to_size");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_to_size",
                table: "product_to_size");

            migrationBuilder.RenameTable(
                name: "product_to_size",
                newName: "ProductSize");

            migrationBuilder.RenameIndex(
                name: "IX_product_to_size_SizesId",
                table: "ProductSize",
                newName: "IX_ProductSize_SizesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSize",
                table: "ProductSize",
                columns: new[] { "ProductsId", "SizesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSize_products_ProductsId",
                table: "ProductSize",
                column: "ProductsId",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSize_sizes_SizesId",
                table: "ProductSize",
                column: "SizesId",
                principalTable: "sizes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
