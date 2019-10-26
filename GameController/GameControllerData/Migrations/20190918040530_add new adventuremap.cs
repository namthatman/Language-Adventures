using Microsoft.EntityFrameworkCore.Migrations;

namespace GameControllerData.Migrations
{
    public partial class addnewadventuremap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "newAdventureMaps",
                columns: table => new
                {
                    AdventureMapID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FromWaypointID = table.Column<int>(nullable: false),
                    ToWaypointID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_newAdventureMaps", x => x.AdventureMapID);
                    table.ForeignKey(
                        name: "FK_newAdventureMaps_Waypoint_FromWaypointID",
                        column: x => x.FromWaypointID,
                        principalTable: "Waypoint",
                        principalColumn: "WaypointID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_newAdventureMaps_Waypoint_ToWaypointID",
                        column: x => x.ToWaypointID,
                        principalTable: "Waypoint",
                        principalColumn: "WaypointID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_newAdventureMaps_FromWaypointID",
                table: "newAdventureMaps",
                column: "FromWaypointID");

            migrationBuilder.CreateIndex(
                name: "IX_newAdventureMaps_ToWaypointID",
                table: "newAdventureMaps",
                column: "ToWaypointID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "newAdventureMaps");

            migrationBuilder.DropColumn(
                name: "ChallengeDetail",
                table: "Challenge");
        }
    }
}
