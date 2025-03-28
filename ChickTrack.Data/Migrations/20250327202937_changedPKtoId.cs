using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChickTrack.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedPKtoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleRecords_FeedSalesUnits_FeedSalesUnitCode",
                table: "SaleRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TotalSales",
                table: "TotalSales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleRecords",
                table: "SaleRecords");

            migrationBuilder.DropIndex(
                name: "IX_SaleRecords_FeedSalesUnitCode",
                table: "SaleRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvestmentSummaries",
                table: "InvestmentSummaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Investments",
                table: "Investments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedSalesUnits",
                table: "FeedSalesUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedLogs",
                table: "FeedLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedInventories",
                table: "FeedInventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EggTransactions",
                table: "EggTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EggManagements",
                table: "EggManagements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EggInventories",
                table: "EggInventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BirdTransactions",
                table: "BirdTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Birds",
                table: "Birds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BirdManagements",
                table: "BirdManagements");

            migrationBuilder.DropColumn(
                name: "FeedSalesUnitCode",
                table: "SaleRecords");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "TotalSales",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "SaleRecords",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "InvestmentSummaries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Investments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "FeedSalesUnits",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "FeedLogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "FeedInventories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "EggTransactions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "EggManagements",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "EggInventories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "BirdTransactions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Birds",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "BirdManagements",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TotalSales",
                table: "TotalSales",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleRecords",
                table: "SaleRecords",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvestmentSummaries",
                table: "InvestmentSummaries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Investments",
                table: "Investments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedSalesUnits",
                table: "FeedSalesUnits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedLogs",
                table: "FeedLogs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedInventories",
                table: "FeedInventories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EggTransactions",
                table: "EggTransactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EggManagements",
                table: "EggManagements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EggInventories",
                table: "EggInventories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BirdTransactions",
                table: "BirdTransactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Birds",
                table: "Birds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BirdManagements",
                table: "BirdManagements",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SaleRecords_FeedSalesUnitId",
                table: "SaleRecords",
                column: "FeedSalesUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleRecords_FeedSalesUnits_FeedSalesUnitId",
                table: "SaleRecords",
                column: "FeedSalesUnitId",
                principalTable: "FeedSalesUnits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleRecords_FeedSalesUnits_FeedSalesUnitId",
                table: "SaleRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TotalSales",
                table: "TotalSales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleRecords",
                table: "SaleRecords");

            migrationBuilder.DropIndex(
                name: "IX_SaleRecords_FeedSalesUnitId",
                table: "SaleRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvestmentSummaries",
                table: "InvestmentSummaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Investments",
                table: "Investments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedSalesUnits",
                table: "FeedSalesUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedLogs",
                table: "FeedLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedInventories",
                table: "FeedInventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EggTransactions",
                table: "EggTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EggManagements",
                table: "EggManagements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EggInventories",
                table: "EggInventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BirdTransactions",
                table: "BirdTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Birds",
                table: "Birds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BirdManagements",
                table: "BirdManagements");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "TotalSales",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "SaleRecords",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "FeedSalesUnitCode",
                table: "SaleRecords",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "InvestmentSummaries",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Investments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "FeedSalesUnits",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "FeedLogs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "FeedInventories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Expenses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "EggTransactions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "EggManagements",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "EggInventories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "BirdTransactions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Birds",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "BirdManagements",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TotalSales",
                table: "TotalSales",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleRecords",
                table: "SaleRecords",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvestmentSummaries",
                table: "InvestmentSummaries",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Investments",
                table: "Investments",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedSalesUnits",
                table: "FeedSalesUnits",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedLogs",
                table: "FeedLogs",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedInventories",
                table: "FeedInventories",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EggTransactions",
                table: "EggTransactions",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EggManagements",
                table: "EggManagements",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EggInventories",
                table: "EggInventories",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BirdTransactions",
                table: "BirdTransactions",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Birds",
                table: "Birds",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BirdManagements",
                table: "BirdManagements",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_SaleRecords_FeedSalesUnitCode",
                table: "SaleRecords",
                column: "FeedSalesUnitCode");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleRecords_FeedSalesUnits_FeedSalesUnitCode",
                table: "SaleRecords",
                column: "FeedSalesUnitCode",
                principalTable: "FeedSalesUnits",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
