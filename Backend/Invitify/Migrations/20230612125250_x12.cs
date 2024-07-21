using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invitify.Migrations
{
    public partial class x12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_eventt_eventt_EventtId",
                table: "eventt");

            migrationBuilder.DropIndex(
                name: "IX_eventt_EventtId",
                table: "eventt");

            migrationBuilder.DropColumn(
                name: "EventtId",
                table: "eventt");

            migrationBuilder.AddColumn<int>(
                name: "EventtId",
                table: "eventSchedule",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_eventSchedule_EventtId",
                table: "eventSchedule",
                column: "EventtId");

            migrationBuilder.AddForeignKey(
                name: "FK_eventSchedule_eventt_EventtId",
                table: "eventSchedule",
                column: "EventtId",
                principalTable: "eventt",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_eventSchedule_eventt_EventtId",
                table: "eventSchedule");

            migrationBuilder.DropIndex(
                name: "IX_eventSchedule_EventtId",
                table: "eventSchedule");

            migrationBuilder.DropColumn(
                name: "EventtId",
                table: "eventSchedule");

            migrationBuilder.AddColumn<int>(
                name: "EventtId",
                table: "eventt",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_eventt_EventtId",
                table: "eventt",
                column: "EventtId");

            migrationBuilder.AddForeignKey(
                name: "FK_eventt_eventt_EventtId",
                table: "eventt",
                column: "EventtId",
                principalTable: "eventt",
                principalColumn: "Id");
        }
    }
}
