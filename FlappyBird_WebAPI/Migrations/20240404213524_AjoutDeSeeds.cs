using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlappyBird_WebAPI.Migrations
{
    public partial class AjoutDeSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Score_AspNetUsers_UserId",
                table: "Score");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Score",
                newName: "_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Score_UserId",
                table: "Score",
                newName: "IX_Score__userId");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "11111111-1111-1111-1111-111111111111", 0, "7d981a43-e11d-4dfd-86ed-b6a66de54d7b", "jean@exemple.com", false, false, null, "JEAN@EXEMPLE.COM", "JEANPIERRE", "AQAAAAEAACcQAAAAEKindY1ZWXd7pAaMfxdwWuldrqJaUUb5LASxMkq3dEeFu6AznDBDircAEUmIiKszlg==", null, false, "ce24f747-0362-409f-be8a-8818ae2d51d1", false, "jeanpierre" },
                    { "11111111-1111-1111-1111-111111111112", 0, "891f27f8-cd34-4d77-9403-f0d5dc741021", "mario@exemple.com", false, false, null, "MARIO@EXEMPLE.COM", "MARIO", "AQAAAAEAACcQAAAAEHDRvcEMX1zEY8DCu4iJD+XyLKU1SOHq278/0llrEztkv505RPwmETIsbttFqylQ2A==", null, false, "c829d9d8-26c4-43ff-be82-028144f4d325", false, "mario" }
                });

            migrationBuilder.InsertData(
                table: "Score",
                columns: new[] { "id", "_userId", "date", "isPublic", "pseudo", "scoreValue", "timeInSeconds" },
                values: new object[,]
                {
                    { 1, null, "2024-04-04 15:45:03", true, "jeanpierre", 298, "300.999" },
                    { 2, null, "2024-04-02 12:39:03", false, "jeanpierre", 3, "5.125" },
                    { 3, null, "2024-04-04 12:39:03", true, "mario", 252, "255.125" },
                    { 4, null, "2024-04-02 12:39:03", false, "mario", 52, "55.125" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Score_AspNetUsers__userId",
                table: "Score",
                column: "_userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Score_AspNetUsers__userId",
                table: "Score");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111112");

            migrationBuilder.DeleteData(
                table: "Score",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Score",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Score",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Score",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "_userId",
                table: "Score",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Score__userId",
                table: "Score",
                newName: "IX_Score_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Score_AspNetUsers_UserId",
                table: "Score",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
