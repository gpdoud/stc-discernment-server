using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stc_discernment_server.Migrations
{
    public partial class addednotetoconfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Configs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Configs");
        }
    }
}
