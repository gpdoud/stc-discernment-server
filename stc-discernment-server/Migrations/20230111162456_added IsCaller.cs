using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stc_discernment_server.Migrations
{
    public partial class addedIsCaller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCaller",
                table: "Parishioners",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCaller",
                table: "Parishioners");
        }
    }
}
