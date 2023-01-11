using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stc_discernment_server.Migrations
{
    public partial class addedcaller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Parishioners",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<int>(
                name: "CallerId",
                table: "Parishioners",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parishioners_CallerId",
                table: "Parishioners",
                column: "CallerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parishioners_Parishioners_CallerId",
                table: "Parishioners",
                column: "CallerId",
                principalTable: "Parishioners",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parishioners_Parishioners_CallerId",
                table: "Parishioners");

            migrationBuilder.DropIndex(
                name: "IX_Parishioners_CallerId",
                table: "Parishioners");

            migrationBuilder.DropColumn(
                name: "CallerId",
                table: "Parishioners");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Parishioners",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);
        }
    }
}
