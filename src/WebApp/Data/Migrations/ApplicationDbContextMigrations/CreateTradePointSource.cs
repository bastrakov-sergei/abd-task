using Microsoft.EntityFrameworkCore.Migrations;
using System;

/* ReSharper disable All */
namespace WebApp.Data.Migrations.ApplicationDbContextMigrations
{
    public partial class CreateTradePointSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATMs_DataFiles_DataFileId",
                table: "ATMs");

            migrationBuilder.DropForeignKey(
                name: "FK_InfoKiosks_DataFiles_DataFileId",
                table: "InfoKiosks");

            migrationBuilder.DropForeignKey(
                name: "FK_Offices_DataFiles_DataFileId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Offices_DataFileId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_InfoKiosks_DataFileId",
                table: "InfoKiosks");

            migrationBuilder.DropIndex(
                name: "IX_ATMs_DataFileId",
                table: "ATMs");

            migrationBuilder.RenameColumn(
                name: "DataFileId",
                table: "Offices",
                newName: "SourceTradePointSourceId");

            migrationBuilder.RenameColumn(
                name: "DataFileId",
                table: "InfoKiosks",
                newName: "SourceTradePointSourceId");

            migrationBuilder.RenameColumn(
                name: "DataFileId",
                table: "ATMs",
                newName: "SourceTradePointSourceId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Offices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceDataFileType",
                table: "Offices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "InfoKiosks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceDataFileType",
                table: "InfoKiosks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "DataFiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ATMs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceDataFileType",
                table: "ATMs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ATMSources",
                columns: table => new
                {
                    TradePointSourceId = table.Column<Guid>(nullable: false),
                    DataFileType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATMSources", x => new { x.TradePointSourceId, x.DataFileType });
                });

            migrationBuilder.CreateTable(
                name: "InfoKioskSources",
                columns: table => new
                {
                    TradePointSourceId = table.Column<Guid>(nullable: false),
                    DataFileType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoKioskSources", x => new { x.TradePointSourceId, x.DataFileType });
                });

            migrationBuilder.CreateTable(
                name: "OfficeSources",
                columns: table => new
                {
                    TradePointSourceId = table.Column<Guid>(nullable: false),
                    DataFileType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeSources", x => new { x.TradePointSourceId, x.DataFileType });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offices_SourceTradePointSourceId_SourceDataFileType",
                table: "Offices",
                columns: new[] { "SourceTradePointSourceId", "SourceDataFileType" },
                unique: true,
                filter: "[SourceTradePointSourceId] IS NOT NULL AND [SourceDataFileType] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InfoKiosks_SourceTradePointSourceId_SourceDataFileType",
                table: "InfoKiosks",
                columns: new[] { "SourceTradePointSourceId", "SourceDataFileType" },
                unique: true,
                filter: "[SourceTradePointSourceId] IS NOT NULL AND [SourceDataFileType] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ATMs_SourceTradePointSourceId_SourceDataFileType",
                table: "ATMs",
                columns: new[] { "SourceTradePointSourceId", "SourceDataFileType" },
                unique: true,
                filter: "[SourceTradePointSourceId] IS NOT NULL AND [SourceDataFileType] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ATMs_ATMSources_SourceTradePointSourceId_SourceDataFileType",
                table: "ATMs",
                columns: new[] { "SourceTradePointSourceId", "SourceDataFileType" },
                principalTable: "ATMSources",
                principalColumns: new[] { "TradePointSourceId", "DataFileType" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InfoKiosks_InfoKioskSources_SourceTradePointSourceId_SourceDataFileType",
                table: "InfoKiosks",
                columns: new[] { "SourceTradePointSourceId", "SourceDataFileType" },
                principalTable: "InfoKioskSources",
                principalColumns: new[] { "TradePointSourceId", "DataFileType" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_OfficeSources_SourceTradePointSourceId_SourceDataFileType",
                table: "Offices",
                columns: new[] { "SourceTradePointSourceId", "SourceDataFileType" },
                principalTable: "OfficeSources",
                principalColumns: new[] { "TradePointSourceId", "DataFileType" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATMs_ATMSources_SourceTradePointSourceId_SourceDataFileType",
                table: "ATMs");

            migrationBuilder.DropForeignKey(
                name: "FK_InfoKiosks_InfoKioskSources_SourceTradePointSourceId_SourceDataFileType",
                table: "InfoKiosks");

            migrationBuilder.DropForeignKey(
                name: "FK_Offices_OfficeSources_SourceTradePointSourceId_SourceDataFileType",
                table: "Offices");

            migrationBuilder.DropTable(
                name: "ATMSources");

            migrationBuilder.DropTable(
                name: "InfoKioskSources");

            migrationBuilder.DropTable(
                name: "OfficeSources");

            migrationBuilder.DropIndex(
                name: "IX_Offices_SourceTradePointSourceId_SourceDataFileType",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_InfoKiosks_SourceTradePointSourceId_SourceDataFileType",
                table: "InfoKiosks");

            migrationBuilder.DropIndex(
                name: "IX_ATMs_SourceTradePointSourceId_SourceDataFileType",
                table: "ATMs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "SourceDataFileType",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "InfoKiosks");

            migrationBuilder.DropColumn(
                name: "SourceDataFileType",
                table: "InfoKiosks");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "DataFiles");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ATMs");

            migrationBuilder.DropColumn(
                name: "SourceDataFileType",
                table: "ATMs");

            migrationBuilder.RenameColumn(
                name: "SourceTradePointSourceId",
                table: "Offices",
                newName: "DataFileId");

            migrationBuilder.RenameColumn(
                name: "SourceTradePointSourceId",
                table: "InfoKiosks",
                newName: "DataFileId");

            migrationBuilder.RenameColumn(
                name: "SourceTradePointSourceId",
                table: "ATMs",
                newName: "DataFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_DataFileId",
                table: "Offices",
                column: "DataFileId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoKiosks_DataFileId",
                table: "InfoKiosks",
                column: "DataFileId");

            migrationBuilder.CreateIndex(
                name: "IX_ATMs_DataFileId",
                table: "ATMs",
                column: "DataFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ATMs_DataFiles_DataFileId",
                table: "ATMs",
                column: "DataFileId",
                principalTable: "DataFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InfoKiosks_DataFiles_DataFileId",
                table: "InfoKiosks",
                column: "DataFileId",
                principalTable: "DataFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_DataFiles_DataFileId",
                table: "Offices",
                column: "DataFileId",
                principalTable: "DataFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
