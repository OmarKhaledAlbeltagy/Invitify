using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invitify.Migrations
{
    public partial class x3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contact_ContactType_ContactTypeId",
                table: "contact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactType",
                table: "ContactType");

            migrationBuilder.RenameTable(
                name: "ContactType",
                newName: "contactType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contactType",
                table: "contactType",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "eventt",
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
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Guidd = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventtId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_eventt_eventt_EventtId",
                        column: x => x.EventtId,
                        principalTable: "eventt",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_eventt_state_StateId",
                        column: x => x.StateId,
                        principalTable: "state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Property = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSocialMedia = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_properties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "eventDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventtId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_eventDates_eventt_EventtId",
                        column: x => x.EventtId,
                        principalTable: "eventt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "eventGallery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventtId = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventGallery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_eventGallery_eventt_EventtId",
                        column: x => x.EventtId,
                        principalTable: "eventt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "eventSpeakers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventtId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventSpeakers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_eventSpeakers_eventt_EventtId",
                        column: x => x.EventtId,
                        principalTable: "eventt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "eventSponsors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventtId = table.Column<int>(type: "int", nullable: false),
                    SponsorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventSponsors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_eventSponsors_eventt_EventtId",
                        column: x => x.EventtId,
                        principalTable: "eventt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "eventSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventDatestId = table.Column<int>(type: "int", nullable: false),
                    eventDatesId = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_eventSchedule_eventDates_eventDatesId",
                        column: x => x.eventDatesId,
                        principalTable: "eventDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_eventDates_EventtId",
                table: "eventDates",
                column: "EventtId");

            migrationBuilder.CreateIndex(
                name: "IX_eventGallery_EventtId",
                table: "eventGallery",
                column: "EventtId");

            migrationBuilder.CreateIndex(
                name: "IX_eventSchedule_eventDatesId",
                table: "eventSchedule",
                column: "eventDatesId");

            migrationBuilder.CreateIndex(
                name: "IX_eventSpeakers_EventtId",
                table: "eventSpeakers",
                column: "EventtId");

            migrationBuilder.CreateIndex(
                name: "IX_eventSponsors_EventtId",
                table: "eventSponsors",
                column: "EventtId");

            migrationBuilder.CreateIndex(
                name: "IX_eventt_EventtId",
                table: "eventt",
                column: "EventtId");

            migrationBuilder.CreateIndex(
                name: "IX_eventt_StateId",
                table: "eventt",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_contact_contactType_ContactTypeId",
                table: "contact",
                column: "ContactTypeId",
                principalTable: "contactType",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contact_contactType_ContactTypeId",
                table: "contact");

            migrationBuilder.DropTable(
                name: "eventGallery");

            migrationBuilder.DropTable(
                name: "eventSchedule");

            migrationBuilder.DropTable(
                name: "eventSpeakers");

            migrationBuilder.DropTable(
                name: "eventSponsors");

            migrationBuilder.DropTable(
                name: "properties");

            migrationBuilder.DropTable(
                name: "eventDates");

            migrationBuilder.DropTable(
                name: "eventt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contactType",
                table: "contactType");

            migrationBuilder.RenameTable(
                name: "contactType",
                newName: "ContactType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactType",
                table: "ContactType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_contact_ContactType_ContactTypeId",
                table: "contact",
                column: "ContactTypeId",
                principalTable: "ContactType",
                principalColumn: "Id");
        }
    }
}
