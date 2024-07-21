using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invitify.Migrations
{
    public partial class x28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "invitees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    eventtId = table.Column<int>(type: "int", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IsEmail = table.Column<bool>(type: "bit", nullable: false),
                    IsSms = table.Column<bool>(type: "bit", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invitees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_invitees_contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_invitees_eventt_eventtId",
                        column: x => x.eventtId,
                        principalTable: "eventt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_invitees_ContactId",
                table: "invitees",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_invitees_eventtId",
                table: "invitees",
                column: "eventtId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "invitees");
        }
    }
}
