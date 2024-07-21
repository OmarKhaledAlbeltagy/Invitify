using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invitify.Migrations
{
    public partial class x18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "eventSponsors",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "eventSponsors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "eventSpeakers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "eventSpeakers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "eventGallery",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "eventGallery",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "eventSponsors");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "eventSponsors");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "eventSpeakers");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "eventSpeakers");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "eventGallery");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "eventGallery");
        }
    }
}
