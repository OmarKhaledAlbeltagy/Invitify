using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invitify.Migrations
{
    public partial class x37 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "attendance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    EventtId = table.Column<int>(type: "int", nullable: false),
                    AttendanceDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExtendIdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attendance_AspNetUsers_ExtendIdentityUserId",
                        column: x => x.ExtendIdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_attendance_contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_attendance_eventt_EventtId",
                        column: x => x.EventtId,
                        principalTable: "eventt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_attendance_ContactId",
                table: "attendance",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_attendance_EventtId",
                table: "attendance",
                column: "EventtId");

            migrationBuilder.CreateIndex(
                name: "IX_attendance_ExtendIdentityUserId",
                table: "attendance",
                column: "ExtendIdentityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attendance");
        }
    }
}
