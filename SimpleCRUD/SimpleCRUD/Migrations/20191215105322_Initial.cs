using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleCRUD.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDateTime = table.Column<DateTime>(nullable: false),
                    CreationUserId = table.Column<long>(nullable: true),
                    LastModificationDateTime = table.Column<DateTime>(nullable: true),
                    LastModificationUserId = table.Column<long>(nullable: true),
                    DeletionFlag = table.Column<bool>(nullable: false),
                    DeletionUserId = table.Column<long>(nullable: true),
                    DeletionDateTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
