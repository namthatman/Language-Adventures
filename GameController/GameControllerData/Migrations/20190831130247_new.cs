using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameControllerData.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Challenge",
                columns: table => new
                {
                    ChallengeID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChallengeType = table.Column<string>(nullable: true),
                    ChallengeDetail = table.Column<string>(nullable: true),
                    CorrectAnswer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenge", x => x.ChallengeID);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longtitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "Waypoint",
                columns: table => new
                {
                    WaypointID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    ChallengeID = table.Column<int?>(nullable: true),
                    AdventureID = table.Column<int?>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waypoint", x => x.WaypointID);
                    table.ForeignKey(
                        name: "FK_Waypoint_Challenge_ChallengeID",
                        column: x => x.ChallengeID,
                        principalTable: "Challenge",
                        principalColumn: "ChallengeID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Time = table.Column<DateTime>(nullable: false),
                    MessageContent = table.Column<string>(nullable: true),
                    TeamID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_Message_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Student_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submission",
                columns: table => new
                {
                    SubmissionID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    TeamID = table.Column<int>(nullable: false),
                    ChallengeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submission", x => x.SubmissionID);
                    table.ForeignKey(
                        name: "FK_Submission_Challenge_ChallengeID",
                        column: x => x.ChallengeID,
                        principalTable: "Challenge",
                        principalColumn: "ChallengeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submission_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adventure",
                columns: table => new
                {
                    AdventureID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CoverImage = table.Column<string>(nullable: true),
                    WaypointID = table.Column<int?>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adventure", x => x.AdventureID);
                    table.ForeignKey(
                        name: "FK_Adventure_Waypoint_WaypointID",
                        column: x => x.WaypointID,
                        principalTable: "Waypoint",
                        principalColumn: "WaypointID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AdventureMap",
                columns: table => new
                {
                    FromWaypointID = table.Column<int>(nullable: false),
                    ToWaypointID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventureMap", x => new { x.FromWaypointID, x.ToWaypointID });
                    table.ForeignKey(
                        name: "FK_AdventureMap_Waypoint_FromWaypointID",
                        column: x => x.FromWaypointID,
                        principalTable: "Waypoint",
                        principalColumn: "WaypointID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdventureMap_Waypoint_ToWaypointID",
                        column: x => x.ToWaypointID,
                        principalTable: "Waypoint",
                        principalColumn: "WaypointID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdventureSession",
                columns: table => new
                {
                    TeamID = table.Column<int>(nullable: false),
                    AdventureID = table.Column<int>(nullable: false),
                    WaypointID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventureSession", x => x.TeamID);
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
                name: "IX_Adventure_WaypointID",
                table: "Adventure",
                column: "WaypointID");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureMap_ToWaypointID",
                table: "AdventureMap",
                column: "ToWaypointID");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureSession_AdventureID",
                table: "AdventureSession",
                column: "AdventureID");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureSession_WaypointID",
                table: "AdventureSession",
                column: "WaypointID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_TeamID",
                table: "Message",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_TeamID",
                table: "Student",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Submission_ChallengeID",
                table: "Submission",
                column: "ChallengeID");

            migrationBuilder.CreateIndex(
                name: "IX_Submission_TeamID",
                table: "Submission",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Waypoint_ChallengeID",
                table: "Waypoint",
                column: "ChallengeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdventureMap");

            migrationBuilder.DropTable(
                name: "AdventureSession");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Submission");

            migrationBuilder.DropTable(
                name: "Adventure");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Waypoint");

            migrationBuilder.DropTable(
                name: "Challenge");
        }
    }
}
