using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stc_discernment_server.Migrations
{
    public partial class changetableconfigstoconfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Configs",
                table: "Configs");

            migrationBuilder.RenameTable(
                name: "Configs",
                newName: "Configurations");

            migrationBuilder.RenameIndex(
                name: "IX_Configs_KeyValue",
                table: "Configurations",
                newName: "IX_Configurations_KeyValue");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Configurations",
                table: "Configurations",
                column: "KeyValue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Configurations",
                table: "Configurations");

            migrationBuilder.RenameTable(
                name: "Configurations",
                newName: "Configs");

            migrationBuilder.RenameIndex(
                name: "IX_Configurations_KeyValue",
                table: "Configs",
                newName: "IX_Configs_KeyValue");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Configs",
                table: "Configs",
                column: "KeyValue");
        }
    }
}
