using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlappyBird_WebAPI.Migrations
{
    public partial class PseudoEtDatePeutEtreNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "date",
                table: "Score",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pseudo",
                table: "Score",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "pseudo",
                table: "Score");
        }
    }
}
