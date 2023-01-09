using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stc_discernment_server.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parishioners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Cellphone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Homephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Ministry = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Reviewed = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 20, nullable: true),
                    SubmittedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parishioners", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parishioners");
        }
    }
}
