using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolicyClaimWebApi.Migrations
{
    public partial class updateClaimType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimId",
                table: "ClaimTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClaimId",
                table: "ClaimTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
