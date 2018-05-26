using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Data.Migrations.ApplicationDbContextMigrations
{
    public partial class AddProcessingErrorColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TradePoints",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcessingError",
                table: "DataFiles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TradePoints_Name",
                table: "TradePoints",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TradePoints_Name",
                table: "TradePoints");

            migrationBuilder.DropColumn(
                name: "ProcessingError",
                table: "DataFiles");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TradePoints",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
