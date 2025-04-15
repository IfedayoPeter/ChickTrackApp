using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChickTrack.Data.Migrations
{
    /// <inheritdoc />
    public partial class droppedEggTransactionFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EggTransactions_AspNetUsers_InvestorId",
                table: "EggTransactions");

            migrationBuilder.DropIndex(
                name: "IX_EggTransactions_InvestorId",
                table: "EggTransactions");

            migrationBuilder.AlterColumn<string>(
                name: "InvestorId",
                table: "EggTransactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InvestorId",
                table: "EggTransactions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EggTransactions_InvestorId",
                table: "EggTransactions",
                column: "InvestorId");

            migrationBuilder.AddForeignKey(
                name: "FK_EggTransactions_AspNetUsers_InvestorId",
                table: "EggTransactions",
                column: "InvestorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
