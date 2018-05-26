using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Data.Migrations.ApplicationDbContextMigrations
{
    public partial class AddTradePointLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Offices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Offices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "InfoKiosks",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "InfoKiosks",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "ATMs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "ATMs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.Sql("ALTER TABLE [dbo].[Offices] ADD [Location] geography");
            migrationBuilder.Sql("ALTER TABLE [dbo].[InfoKiosks] ADD [Location] geography");
            migrationBuilder.Sql("ALTER TABLE [dbo].[ATMs] ADD [Location] geography");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "InfoKiosks");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "InfoKiosks");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "ATMs");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "ATMs");

            migrationBuilder.Sql("ALTER TABLE [dbo].[Offices] DROP COLUMN [Location]");
            migrationBuilder.Sql("ALTER TABLE [dbo].[InfoKiosks] DROP COLUMN [Location]");
            migrationBuilder.Sql("ALTER TABLE [dbo].[ATMs] DROP COLUMN [Location]");
        }
    }
}
