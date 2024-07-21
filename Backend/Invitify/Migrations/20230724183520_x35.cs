using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invitify.Migrations
{
    public partial class x35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eventEntryOrganizer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventtId = table.Column<int>(type: "int", nullable: false),
                    ExtendIdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventEntryOrganizer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_eventEntryOrganizer_AspNetUsers_ExtendIdentityUserId",
                        column: x => x.ExtendIdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_eventEntryOrganizer_eventt_EventtId",
                        column: x => x.EventtId,
                        principalTable: "eventt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_eventEntryOrganizer_EventtId",
                table: "eventEntryOrganizer",
                column: "EventtId");

            migrationBuilder.CreateIndex(
                name: "IX_eventEntryOrganizer_ExtendIdentityUserId",
                table: "eventEntryOrganizer",
                column: "ExtendIdentityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eventEntryOrganizer");
        }
    }
}
