using Microsoft.EntityFrameworkCore.Migrations;
using System;

/* ReSharper disable All */
namespace WebApp.Data.Migrations.ApplicationDbContextMigrations
{
    public partial class AddTradePointsSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_DataFiles_Hash_Id",
                table: "DataFiles");

            migrationBuilder.DropIndex(
                name: "IX_DataFiles_Hash",
                table: "DataFiles");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Hash",
                table: "DataFiles",
                nullable: true,
                oldClrType: typeof(byte[]));

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "DataFiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ATMs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataFileId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ATMs_DataFiles_DataFileId",
                        column: x => x.DataFileId,
                        principalTable: "DataFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InfoKiosks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataFileId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoKiosks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoKiosks_DataFiles_DataFileId",
                        column: x => x.DataFileId,
                        principalTable: "DataFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataFileId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offices_DataFiles_DataFileId",
                        column: x => x.DataFileId,
                        principalTable: "DataFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataFiles_Hash",
                table: "DataFiles",
                column: "Hash",
                unique: true,
                filter: "[Hash] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ATMs_DataFileId",
                table: "ATMs",
                column: "DataFileId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoKiosks_DataFileId",
                table: "InfoKiosks",
                column: "DataFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_DataFileId",
                table: "Offices",
                column: "DataFileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ATMs");

            migrationBuilder.DropTable(
                name: "InfoKiosks");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_DataFiles_Hash",
                table: "DataFiles");

            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "DataFiles");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Hash",
                table: "DataFiles",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_DataFiles_Hash_Id",
                table: "DataFiles",
                columns: new[] { "Hash", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_DataFiles_Hash",
                table: "DataFiles",
                column: "Hash",
                unique: true);
        }
    }
}
