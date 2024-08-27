using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamTable233 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Teams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_PlayerId",
                table: "Teams",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Players_PlayerId",
                table: "Teams",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Players_PlayerId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_PlayerId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Teams");
        }
    }
}
