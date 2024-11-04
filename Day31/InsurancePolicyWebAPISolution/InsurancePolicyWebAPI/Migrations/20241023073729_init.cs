using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsurancePolicyWebAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Claimants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claimants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClaimTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    PolicyNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PolicyDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.PolicyNumber);
                    table.ForeignKey(
                        name: "FK_Policies_Claimants_ClaimantId",
                        column: x => x.ClaimantId,
                        principalTable: "Claimants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimTypeId = table.Column<int>(type: "int", nullable: false),
                    ClaimantId = table.Column<int>(type: "int", nullable: false),
                    DateOfIncident = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PolicyNumber = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_Claimants_ClaimantId",
                        column: x => x.ClaimantId,
                        principalTable: "Claimants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Claims_ClaimTypes_ClaimTypeId",
                        column: x => x.ClaimTypeId,
                        principalTable: "ClaimTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Claims_Policies_PolicyNumber",
                        column: x => x.PolicyNumber,
                        principalTable: "Policies",
                        principalColumn: "PolicyNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClaimReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    ReportDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaimReports_Claims_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "Claims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimReports_ClaimId",
                table: "ClaimReports",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ClaimantId",
                table: "Claims",
                column: "ClaimantId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ClaimTypeId",
                table: "Claims",
                column: "ClaimTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_PolicyNumber",
                table: "Claims",
                column: "PolicyNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_ClaimantId",
                table: "Policies",
                column: "ClaimantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimReports");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "ClaimTypes");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "Claimants");
        }
    }
}
