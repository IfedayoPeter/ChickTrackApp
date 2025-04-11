using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChickTrack.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateBirdTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirdsLost",
                table: "BirdTransactions");

            migrationBuilder.DropColumn(
                name: "BirdsSold",
                table: "BirdTransactions");

            migrationBuilder.RenameColumn(
                name: "PersonalConsumption",
                table: "BirdTransactions",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "HatchedBirds",
                table: "BirdTransactions",
                newName: "ActionType");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BirdTransactions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "BirdTransactions");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "BirdTransactions",
                newName: "PersonalConsumption");

            migrationBuilder.RenameColumn(
                name: "ActionType",
                table: "BirdTransactions",
                newName: "HatchedBirds");

            migrationBuilder.AddColumn<int>(
                name: "BirdsLost",
                table: "BirdTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BirdsSold",
                table: "BirdTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
