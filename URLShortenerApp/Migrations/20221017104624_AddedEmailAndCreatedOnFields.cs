using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace URLShortenerApp.Migrations
{
    public partial class AddedEmailAndCreatedOnFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserMasterModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ShortUrls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserMasterModels");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ShortUrls");
        }
    }
}
