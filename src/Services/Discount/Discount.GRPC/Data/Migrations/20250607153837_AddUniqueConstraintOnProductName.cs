using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Discount.GRPC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintOnProductName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Coupons_ProductName",
                table: "Coupons",
                column: "ProductName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coupons_ProductName",
                table: "Coupons");
        }
    }
}
