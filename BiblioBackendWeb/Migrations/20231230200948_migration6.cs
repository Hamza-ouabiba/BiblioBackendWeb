using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BiblioApp.Migrations
{
    public partial class migration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "periode",
                table: "Reservation",
                newName: "dateFin");

            migrationBuilder.AddColumn<DateTime>(
                name: "dateDebut",
                table: "Reservation",
                type: "date",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dateDebut",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "dateFin",
                table: "Reservation",
                newName: "periode");
        }
    }
}
