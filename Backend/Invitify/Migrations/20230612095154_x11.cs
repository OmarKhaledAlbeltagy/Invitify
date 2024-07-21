using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invitify.Migrations
{
    public partial class x11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contact_country_PhoneCodeId",
                table: "contact");

            migrationBuilder.AlterColumn<int>(
                name: "PhoneCodeId",
                table: "contact",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_contact_country_PhoneCodeId",
                table: "contact",
                column: "PhoneCodeId",
                principalTable: "country",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contact_country_PhoneCodeId",
                table: "contact");

            migrationBuilder.AlterColumn<int>(
                name: "PhoneCodeId",
                table: "contact",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_contact_country_PhoneCodeId",
                table: "contact",
                column: "PhoneCodeId",
                principalTable: "country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
