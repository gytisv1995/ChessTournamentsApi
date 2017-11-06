using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ChessTournamentsApi.Migrations
{
    public partial class tournamentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBegun",
                table: "Tournaments");

            migrationBuilder.AddColumn<bool>(
                name: "hasEnded",
                table: "Tournaments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hasEnded",
                table: "Tournaments");

            migrationBuilder.AddColumn<bool>(
                name: "HasBegun",
                table: "Tournaments",
                nullable: false,
                defaultValue: false);
        }
    }
}
