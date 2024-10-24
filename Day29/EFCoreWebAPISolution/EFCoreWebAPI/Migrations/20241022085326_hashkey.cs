using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreWebAPI.Migrations
{
    public partial class hashkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "HashKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HashKey",
                table: "Users",
                newName: "PasswordHash");
        }
    }
}
