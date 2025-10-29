using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChickTrack.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuditableEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TotalSales",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TotalSales",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "SaleRecords",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SaleRecords",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Investments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Investments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FeedUnitPrices",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "FeedSalesUnits",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FeedSalesUnits",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FeedPrices",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "FeedLogs",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FeedLogs",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "FeedInventories",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FeedInventories",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Expenses",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Expenses",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "EggTransactions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EggTransactions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "EggInventories",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EggInventories",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BirdTransactions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BirdTransactions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Birds",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Birds",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TotalSales");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TotalSales");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "SaleRecords");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SaleRecords");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Investments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Investments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FeedUnitPrices");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "FeedSalesUnits");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FeedSalesUnits");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FeedPrices");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "FeedLogs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FeedLogs");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "FeedInventories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FeedInventories");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "EggTransactions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EggTransactions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "EggInventories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EggInventories");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BirdTransactions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BirdTransactions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Birds");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Birds");
        }
    }
}
