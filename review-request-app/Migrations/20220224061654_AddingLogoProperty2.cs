using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace review_request_app.Migrations
{
    public partial class AddingLogoProperty2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Clients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Logo",
                table: "Clients",
                type: "varbinary(8000)",
                maxLength: 8000,
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
