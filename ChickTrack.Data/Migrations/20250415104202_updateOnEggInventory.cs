using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChickTrack.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateOnEggInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EggInventories_AspNetUsers_InvestorId",
                table: "EggInventories");

            migrationBuilder.DropIndex(
                name: "IX_EggInventories_InvestorId",
                table: "EggInventories");

            migrationBuilder.DropColumn(
                name: "InvestorId",
                table: "EggInventories");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "EggInventories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "EggInventories");

            migrationBuilder.AddColumn<string>(
                name: "InvestorId",
                table: "EggInventories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_EggInventories_InvestorId",
                table: "EggInventories",
                column: "InvestorId");

            migrationBuilder.AddForeignKey(
                name: "FK_EggInventories_AspNetUsers_InvestorId",
                table: "EggInventories",
                column: "InvestorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
