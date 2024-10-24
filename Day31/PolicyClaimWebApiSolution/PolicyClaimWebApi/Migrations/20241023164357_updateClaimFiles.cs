using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolicyClaimWebApi.Migrations
{
    public partial class updateClaimFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClaimFiles",
                columns: table => new
                {
                    ClaimFileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimID = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimFiles", x => x.ClaimFileId);
                    table.ForeignKey(
                        name: "FK_ClaimFile_Claim",
                        column: x => x.ClaimID,
                        principalTable: "Claims",
                        principalColumn: "ClaimID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimFiles_ClaimID",
                table: "ClaimFiles",
                column: "ClaimID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimFiles");
        }
    }
}
