using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRentWebAPI.Migrations
{
    public partial class addMovieTrailer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrailerVideo",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrailerVideo",
                table: "Movies");
        }
    }
}
