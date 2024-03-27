using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItExpertSolution.Server.Migrations
{
    public partial class InitData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Solutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordering = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solutions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_Code",
                table: "Solutions",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_Ordering",
                table: "Solutions",
                column: "Ordering");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Solutions");
        }
    }
}
