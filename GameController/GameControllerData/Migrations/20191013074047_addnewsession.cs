using Microsoft.EntityFrameworkCore.Migrations;

namespace GameControllerData.Migrations
{
    public partial class addnewsession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    SessionID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamID = table.Column<int>(nullable: false),
                    AdventureID = table.Column<int>(nullable: false),
                    WaypointID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventureSession", x => x.SessionID);
                    table.ForeignKey(
                        name: "FK_AdventureSession_Adventure_AdventureID",
                        column: x => x.AdventureID,
                        principalTable: "Adventure",
                        principalColumn: "AdventureID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdventureSession_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdventureSession_Waypoint_WaypointID",
                        column: x => x.WaypointID,
                        principalTable: "Waypoint",
                        principalColumn: "WaypointID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Session_SessionID",
                table: "Session",
                column: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_Session_TeamID",
                table: "Session",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Session_AdventureID",
                table: "Session",
                column: "AdventureID");

            migrationBuilder.CreateIndex(
                name: "IX_Session_WaypointID",
                table: "Session",
                column: "WaypointID");
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Session");
        }
    }
}
