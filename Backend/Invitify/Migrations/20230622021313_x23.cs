using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invitify.Migrations
{
    public partial class x23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eventSchedule");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eventSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    eventDatesId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventDatestId = table.Column<int>(type: "int", nullable: false),
                    EventtId = table.Column<int>(type: "int", nullable: true),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_eventSchedule_eventt_EventtId",
                        column: x => x.EventtId,
                        principalTable: "eventt",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_eventSchedule_eventDatesId",
                table: "eventSchedule",
                column: "eventDatesId");

            migrationBuilder.CreateIndex(
                name: "IX_eventSchedule_EventtId",
                table: "eventSchedule",
                column: "EventtId");
        }
    }
}
