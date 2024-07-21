using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invitify.Migrations
{
    public partial class x27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "tempEventt");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "eventt");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "tempEventt",
                newName: "IframeLocation");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "eventt",
                newName: "IframeLocation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IframeLocation",
                table: "tempEventt",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "IframeLocation",
                table: "eventt",
                newName: "Longitude");

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "tempEventt",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "eventt",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
