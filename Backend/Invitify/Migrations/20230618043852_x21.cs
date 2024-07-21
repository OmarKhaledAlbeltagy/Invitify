using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invitify.Migrations
{
    public partial class x21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tempEventt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Participants = table.Column<int>(type: "int", nullable: true),
                    Speakers = table.Column<int>(type: "int", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tempEventt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tempEventt_state_StateId",
                        column: x => x.StateId,
                        principalTable: "state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tempEventDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TempEventtId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tempEventDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tempEventDates_tempEventt_TempEventtId",
                        column: x => x.TempEventtId,
                        principalTable: "tempEventt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tempEventGallery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TempEventtId = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tempEventGallery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tempEventGallery_tempEventt_TempEventtId",
                        column: x => x.TempEventtId,
                        principalTable: "tempEventt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tempEventSpeaker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TempEventtId = table.Column<int>(type: "int", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tempEventSpeaker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tempEventSpeaker_tempEventt_TempEventtId",
                        column: x => x.TempEventtId,
                        principalTable: "tempEventt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tempEventSponsor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TempEventtId = table.Column<int>(type: "int", nullable: false),
                    SponsorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tempEventSponsor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tempEventSponsor_tempEventt_TempEventtId",
                        column: x => x.TempEventtId,
                        principalTable: "tempEventt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tempEventSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TempEventDatesId = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TempEventtId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tempEventSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tempEventSchedule_tempEventDates_TempEventDatesId",
                        column: x => x.TempEventDatesId,
                        principalTable: "tempEventDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tempEventSchedule_tempEventt_TempEventtId",
                        column: x => x.TempEventtId,
                        principalTable: "tempEventt",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tempEventDates_TempEventtId",
                table: "tempEventDates",
                column: "TempEventtId");

            migrationBuilder.CreateIndex(
                name: "IX_tempEventGallery_TempEventtId",
                table: "tempEventGallery",
                column: "TempEventtId");

            migrationBuilder.CreateIndex(
                name: "IX_tempEventSchedule_TempEventDatesId",
                table: "tempEventSchedule",
                column: "TempEventDatesId");

            migrationBuilder.CreateIndex(
                name: "IX_tempEventSchedule_TempEventtId",
                table: "tempEventSchedule",
                column: "TempEventtId");

            migrationBuilder.CreateIndex(
                name: "IX_tempEventSpeaker_TempEventtId",
                table: "tempEventSpeaker",
                column: "TempEventtId");

            migrationBuilder.CreateIndex(
                name: "IX_tempEventSponsor_TempEventtId",
                table: "tempEventSponsor",
                column: "TempEventtId");

            migrationBuilder.CreateIndex(
                name: "IX_tempEventt_StateId",
                table: "tempEventt",
                column: "StateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tempEventGallery");

            migrationBuilder.DropTable(
                name: "tempEventSchedule");

            migrationBuilder.DropTable(
                name: "tempEventSpeaker");

            migrationBuilder.DropTable(
                name: "tempEventSponsor");

            migrationBuilder.DropTable(
                name: "tempEventDates");

            migrationBuilder.DropTable(
                name: "tempEventt");
        }
    }
}
