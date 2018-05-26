using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebApp.Data.Migrations.ApplicationDbContextMigrations
{
    public partial class RemoveSourceProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "SourceDataFileType",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "SourceTradePointSourceId",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "SourceDataFileType",
                table: "InfoKiosks");

            migrationBuilder.DropColumn(
                name: "SourceTradePointSourceId",
                table: "InfoKiosks");

            migrationBuilder.DropColumn(
                name: "SourceDataFileType",
                table: "ATMs");

            migrationBuilder.DropColumn(
                name: "SourceTradePointSourceId",
                table: "ATMs");

            migrationBuilder.AddColumn<Guid>(
                name: "TradePointId",
                table: "OfficeSources",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TradePointId",
                table: "InfoKioskSources",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TradePointId",
                table: "ATMSources",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OfficeSources_TradePointId",
                table: "OfficeSources",
                column: "TradePointId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoKioskSources_TradePointId",
                table: "InfoKioskSources",
                column: "TradePointId");

            migrationBuilder.CreateIndex(
                name: "IX_ATMSources_TradePointId",
                table: "ATMSources",
                column: "TradePointId");

            migrationBuilder.AddForeignKey(
                name: "FK_ATMSources_ATMs_TradePointId",
                table: "ATMSources",
                column: "TradePointId",
                principalTable: "ATMs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InfoKioskSources_InfoKiosks_TradePointId",
                table: "InfoKioskSources",
                column: "TradePointId",
                principalTable: "InfoKiosks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficeSources_Offices_TradePointId",
                table: "OfficeSources",
                column: "TradePointId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATMSources_ATMs_TradePointId",
                table: "ATMSources");

            migrationBuilder.DropForeignKey(
                name: "FK_InfoKioskSources_InfoKiosks_TradePointId",
                table: "InfoKioskSources");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficeSources_Offices_TradePointId",
                table: "OfficeSources");

            migrationBuilder.DropIndex(
                name: "IX_OfficeSources_TradePointId",
                table: "OfficeSources");

            migrationBuilder.DropIndex(
                name: "IX_InfoKioskSources_TradePointId",
                table: "InfoKioskSources");

            migrationBuilder.DropIndex(
                name: "IX_ATMSources_TradePointId",
                table: "ATMSources");

            migrationBuilder.DropColumn(
                name: "TradePointId",
                table: "OfficeSources");

            migrationBuilder.DropColumn(
                name: "TradePointId",
                table: "InfoKioskSources");

            migrationBuilder.DropColumn(
                name: "TradePointId",
                table: "ATMSources");

            migrationBuilder.AddColumn<string>(
                name: "SourceDataFileType",
                table: "Offices",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SourceTradePointSourceId",
                table: "Offices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceDataFileType",
                table: "InfoKiosks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SourceTradePointSourceId",
                table: "InfoKiosks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceDataFileType",
                table: "ATMs",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SourceTradePointSourceId",
                table: "ATMs",
                nullable: true);

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
    }
}
