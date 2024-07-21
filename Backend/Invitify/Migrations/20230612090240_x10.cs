using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invitify.Migrations
{
    public partial class x10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhoneCodeId",
                table: "contact",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_contact_PhoneCodeId",
                table: "contact",
                column: "PhoneCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_contact_country_PhoneCodeId",
                table: "contact",
                column: "PhoneCodeId",
                principalTable: "country",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contact_country_PhoneCodeId",
                table: "contact");

            migrationBuilder.DropIndex(
                name: "IX_contact_PhoneCodeId",
                table: "contact");

            migrationBuilder.DropColumn(
                name: "PhoneCodeId",
                table: "contact");
        }
    }
}
