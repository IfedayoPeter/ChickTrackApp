using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChickTrack.Data.Migrations
{
    /// <inheritdoc />
    public partial class salesRecordAndFeedInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BagsSold",
                table: "FeedInventories");

            migrationBuilder.AlterColumn<decimal>(
                name: "BagsSold",
                table: "TotalSales",
                type: "decimal(18,9)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "BagsSold",
                table: "FeedLogs",
                type: "decimal(18,9)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "BagsBought",
                table: "FeedLogs",
                type: "decimal(18,9)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "AvailableBags",
                table: "FeedLogs",
                type: "decimal(18,9)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "BagsBought",
                table: "FeedInventories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BagsBought",
                table: "FeedInventories");

            migrationBuilder.AlterColumn<int>(
                name: "BagsSold",
                table: "TotalSales",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)");

            migrationBuilder.AlterColumn<int>(
                name: "BagsSold",
                table: "FeedLogs",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)");

            migrationBuilder.AlterColumn<int>(
                name: "BagsBought",
                table: "FeedLogs",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)");

            migrationBuilder.AlterColumn<int>(
                name: "AvailableBags",
                table: "FeedLogs",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)");

            migrationBuilder.AddColumn<int>(
                name: "BagsSold",
                table: "FeedInventories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
