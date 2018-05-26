using Microsoft.EntityFrameworkCore.Migrations;
using System;

/* ReSharper disable All */
namespace WebApp.Data.Migrations.ApplicationDbContextMigrations
{
    public partial class CreateDataFilesSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<byte[]>(nullable: true),
                    Hash = table.Column<byte[]>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataFiles", x => x.Id);
                    table.UniqueConstraint("AK_DataFiles_Hash_Id", x => new { x.Hash, x.Id });
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataFiles_Hash",
                table: "DataFiles",
                column: "Hash",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataFiles");
        }
    }
}
