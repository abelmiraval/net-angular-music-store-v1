using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.DataAccess.Migrations
{
    public partial class InitialDataGenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "Id", "Description" },
                values: new object[] { 1, "Rock" });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "Id", "Description" },
                values: new object[] { 2, "Salsa" });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "Id", "Description" },
                values: new object[] { 3, "Reggeaton" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
