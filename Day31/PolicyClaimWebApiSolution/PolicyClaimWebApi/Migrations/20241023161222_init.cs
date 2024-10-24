using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolicyClaimWebApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Claimants",
                columns: table => new
                {
                    ClaimantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claimants", x => x.ClaimantId);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    PolicyNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AboutPolicy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.PolicyNumber);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    ClaimID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimantId = table.Column<int>(type: "int", nullable: false),
                    ClaimDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.ClaimID);
                    table.ForeignKey(
                        name: "FK_Claim_Claimant",
                        column: x => x.ClaimantId,
                        principalTable: "Claimants",
                        principalColumn: "ClaimantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Claim_Policy",
                        column: x => x.PolicyNumber,
                        principalTable: "Policies",
                        principalColumn: "PolicyNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClaimTypes",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    PolicyNumber = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimTypes", x => x.TypeId);
                    table.ForeignKey(
                        name: "FK_Policy_ClaimType",
                        column: x => x.PolicyNumber,
                        principalTable: "Policies",
                        principalColumn: "PolicyNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ClaimantId",
                table: "Claims",
                column: "ClaimantId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_PolicyNumber",
                table: "Claims",
                column: "PolicyNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimTypes_PolicyNumber",
                table: "ClaimTypes",
                column: "PolicyNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "ClaimTypes");

            migrationBuilder.DropTable(
                name: "Claimants");

            migrationBuilder.DropTable(
                name: "Policies");
        }
    }
}
