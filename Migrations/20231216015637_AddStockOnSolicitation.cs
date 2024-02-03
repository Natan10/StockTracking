using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTracking.Migrations
{
    /// <inheritdoc />
    public partial class AddStockOnSolicitation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StockId",
                table: "Solicitations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitations_StockId",
                table: "Solicitations",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitations_Stocks_StockId",
                table: "Solicitations",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitations_Stocks_StockId",
                table: "Solicitations");

            migrationBuilder.DropIndex(
                name: "IX_Solicitations_StockId",
                table: "Solicitations");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "Solicitations");
        }
    }
}
