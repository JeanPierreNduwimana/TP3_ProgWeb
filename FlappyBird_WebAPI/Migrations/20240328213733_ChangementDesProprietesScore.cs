using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlappyBird_WebAPI.Migrations
{
    public partial class ChangementDesProprietesScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Score",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Visibilité",
                table: "Score",
                newName: "isPublic");

            migrationBuilder.RenameColumn(
                name: "Temps",
                table: "Score",
                newName: "timeInSeconds");

            migrationBuilder.RenameColumn(
                name: "Score_Joueur",
                table: "Score",
                newName: "scoreValue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Score",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "timeInSeconds",
                table: "Score",
                newName: "Temps");

            migrationBuilder.RenameColumn(
                name: "scoreValue",
                table: "Score",
                newName: "Score_Joueur");

            migrationBuilder.RenameColumn(
                name: "isPublic",
                table: "Score",
                newName: "Visibilité");
        }
    }
}
