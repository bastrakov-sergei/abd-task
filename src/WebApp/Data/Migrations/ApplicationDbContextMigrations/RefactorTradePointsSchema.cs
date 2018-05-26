using Microsoft.EntityFrameworkCore.Migrations;
using System;

/* ReSharper disable All */
namespace WebApp.Data.Migrations.ApplicationDbContextMigrations
{
    public partial class RefactorTradePointsSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ATMSources");

            migrationBuilder.DropTable(
                name: "InfoKioskSources");

            migrationBuilder.DropTable(
                name: "OfficeSources");

            migrationBuilder.DropTable(
                name: "ATMs");

            migrationBuilder.DropTable(
                name: "InfoKiosks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offices",
                table: "Offices");

            migrationBuilder.RenameTable(
                name: "Offices",
                newName: "TradePoints");

            migrationBuilder.AddColumn<Guid>(
                name: "TypeId",
                table: "TradePoints",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TradePoints",
                table: "TradePoints",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TradePointSources",
                columns: table => new
                {
                    TradePointSourceId = table.Column<Guid>(nullable: false),
                    DataFileType = table.Column<string>(nullable: false),
                    TradePointId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradePointSources", x => new { x.TradePointSourceId, x.DataFileType });
                    table.ForeignKey(
                        name: "FK_TradePointSources_TradePoints_TradePointId",
                        column: x => x.TradePointId,
                        principalTable: "TradePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TradePointTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradePointTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TradePoints_TypeId",
                table: "TradePoints",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TradePointSources_TradePointId",
                table: "TradePointSources",
                column: "TradePointId");

            migrationBuilder.AddForeignKey(
                name: "FK_TradePoints_TradePointTypes_TypeId",
                table: "TradePoints",
                column: "TypeId",
                principalTable: "TradePointTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TradePoints_TradePointTypes_TypeId",
                table: "TradePoints");

            migrationBuilder.DropTable(
                name: "TradePointSources");

            migrationBuilder.DropTable(
                name: "TradePointTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TradePoints",
                table: "TradePoints");

            migrationBuilder.DropIndex(
                name: "IX_TradePoints_TypeId",
                table: "TradePoints");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "TradePoints");

            migrationBuilder.RenameTable(
                name: "TradePoints",
                newName: "Offices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offices",
                table: "Offices",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ATMs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InfoKiosks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoKiosks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficeSources",
                columns: table => new
                {
                    TradePointSourceId = table.Column<Guid>(nullable: false),
                    DataFileType = table.Column<string>(nullable: false),
                    TradePointId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeSources", x => new { x.TradePointSourceId, x.DataFileType });
                    table.ForeignKey(
                        name: "FK_OfficeSources_Offices_TradePointId",
                        column: x => x.TradePointId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ATMSources",
                columns: table => new
                {
                    TradePointSourceId = table.Column<Guid>(nullable: false),
                    DataFileType = table.Column<string>(nullable: false),
                    TradePointId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATMSources", x => new { x.TradePointSourceId, x.DataFileType });
                    table.ForeignKey(
                        name: "FK_ATMSources_ATMs_TradePointId",
                        column: x => x.TradePointId,
                        principalTable: "ATMs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InfoKioskSources",
                columns: table => new
                {
                    TradePointSourceId = table.Column<Guid>(nullable: false),
                    DataFileType = table.Column<string>(nullable: false),
                    TradePointId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoKioskSources", x => new { x.TradePointSourceId, x.DataFileType });
                    table.ForeignKey(
                        name: "FK_InfoKioskSources_InfoKiosks_TradePointId",
                        column: x => x.TradePointId,
                        principalTable: "InfoKiosks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ATMSources_TradePointId",
                table: "ATMSources",
                column: "TradePointId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoKioskSources_TradePointId",
                table: "InfoKioskSources",
                column: "TradePointId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficeSources_TradePointId",
                table: "OfficeSources",
                column: "TradePointId");
        }
    }
}
