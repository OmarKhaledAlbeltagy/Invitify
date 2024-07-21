using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invitify.Migrations
{
    public partial class x8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contact_contactType_ContactTypeId",
                table: "contact");

            migrationBuilder.DropForeignKey(
                name: "FK_contact_state_StateId",
                table: "contact");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "contact",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContactTypeId",
                table: "contact",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_contact_contactType_ContactTypeId",
                table: "contact",
                column: "ContactTypeId",
                principalTable: "contactType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_contact_state_StateId",
                table: "contact",
                column: "StateId",
                principalTable: "state",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contact_contactType_ContactTypeId",
                table: "contact");

            migrationBuilder.DropForeignKey(
                name: "FK_contact_state_StateId",
                table: "contact");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "contact",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ContactTypeId",
                table: "contact",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_contact_contactType_ContactTypeId",
                table: "contact",
                column: "ContactTypeId",
                principalTable: "contactType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_contact_state_StateId",
                table: "contact",
                column: "StateId",
                principalTable: "state",
                principalColumn: "Id");
        }
    }
}
