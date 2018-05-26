using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Data.Migrations.ApplicationDbContextMigrations
{
    public partial class RemoveNameConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TradePoints_Name",
                table: "TradePoints");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TradePointTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TradePoints",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TradePointTypes_Name",
                table: "TradePointTypes",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TradePointTypes_Name",
                table: "TradePointTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TradePointTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TradePoints",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TradePoints_Name",
                table: "TradePoints",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }
    }
}
