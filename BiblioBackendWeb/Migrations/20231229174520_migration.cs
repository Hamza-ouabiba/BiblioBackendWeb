using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BiblioApp.Migrations
{
    public partial class migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Employe",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "genre",
                table: "Employe",
                nullable: false,
                defaultValue: 0);
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "Employe");

            migrationBuilder.DropColumn(
                name: "genre",
                table: "Employe");

        }
    }
}
