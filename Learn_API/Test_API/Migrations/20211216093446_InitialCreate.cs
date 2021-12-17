using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace Test_API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.BlogId);
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "BlogId", "IsDeleted", "Rating", "Url" },
                values: new object[] { 11, true, 4, "test.com" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "BlogId", "IsDeleted", "Rating", "Url" },
                values: new object[] { 12, false, 5, "dev.com" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "BlogId", "IsDeleted", "Rating", "Url" },
                values: new object[] { 13, false, 3, "learnAPI.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}
