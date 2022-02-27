using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace review_request_app.Migrations
{
    public partial class UpdateLogoProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Logo",
                table: "Clients",
                type: "varbinary",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Clients");
        }
    }
}
