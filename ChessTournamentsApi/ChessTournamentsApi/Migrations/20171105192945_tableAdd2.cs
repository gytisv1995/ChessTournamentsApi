using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ChessTournamentsApi.Migrations
{
    public partial class tableAdd2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
              name: "Players",
              columns: table => new
              {
                  Id = table.Column<long>(type: "bigint", nullable: false)
                      .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                  Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  Points = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Players", x => x.Id);
              });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
