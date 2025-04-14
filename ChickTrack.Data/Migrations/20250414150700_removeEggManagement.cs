using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChickTrack.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeEggManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EggTransactions_AspNetUsers_InvestorId",
                table: "EggTransactions");

            migrationBuilder.DropTable(
                name: "EggManagements");

            migrationBuilder.DropColumn(
                name: "Hatched",
                table: "EggTransactions");

            migrationBuilder.RenameColumn(
                name: "Sold",
                table: "EggTransactions",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "PersonalConsumption",
                table: "EggTransactions",
                newName: "ActionType");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "EggTransactions",
                newName: "Price");

            migrationBuilder.AlterColumn<string>(
                name: "InvestorId",
                table: "EggTransactions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_EggTransactions_AspNetUsers_InvestorId",
                table: "EggTransactions",
                column: "InvestorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EggTransactions_AspNetUsers_InvestorId",
                table: "EggTransactions");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "EggTransactions",
                newName: "Sold");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "EggTransactions",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "ActionType",
                table: "EggTransactions",
                newName: "PersonalConsumption");

            migrationBuilder.AlterColumn<string>(
                name: "InvestorId",
                table: "EggTransactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Hatched",
                table: "EggTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EggManagements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvestorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActionType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EggInventoryId = table.Column<long>(type: "bigint", nullable: false),
                    EggTransactionId = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EggManagements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EggManagements_AspNetUsers_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EggManagements_InvestorId",
                table: "EggManagements",
                column: "InvestorId");

            migrationBuilder.AddForeignKey(
                name: "FK_EggTransactions_AspNetUsers_InvestorId",
                table: "EggTransactions",
                column: "InvestorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
