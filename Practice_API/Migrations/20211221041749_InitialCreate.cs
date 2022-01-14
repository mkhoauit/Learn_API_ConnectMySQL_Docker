using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace Practice_API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    IsMale = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.AnimalId);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FoodName = table.Column<string>(type: "text", nullable: true),
                    NumberofCans = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.FoodId);
                });

            migrationBuilder.CreateTable(
                name: "FoodDistributions",
                columns: table => new
                {
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsEnough = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodDistributions", x => new { x.AnimalId, x.FoodId });
                    table.ForeignKey(
                        name: "FK_FoodDistributions_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "AnimalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodDistributions_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "AnimalId", "IsMale", "Name", "Type" },
                values: new object[,]
                {
                    { 1, true, "Tom", "Dog" },
                    { 2, true, "Jerry", "Mouse" },
                    { 3, false, "MiMi", "Cat" }
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "FoodId", "FoodName", "NumberofCans" },
                values: new object[,]
                {
                    { 1, "Meat", 50 },
                    { 2, "Fish", 40 }
                });

            migrationBuilder.InsertData(
                table: "FoodDistributions",
                columns: new[] { "AnimalId", "FoodId", "IsEnough", "Quantity" },
                values: new object[] { 1, 1, false, 60 });

            migrationBuilder.InsertData(
                table: "FoodDistributions",
                columns: new[] { "AnimalId", "FoodId", "IsEnough", "Quantity" },
                values: new object[] { 3, 2, true, 30 });

            migrationBuilder.CreateIndex(
                name: "IX_FoodDistributions_FoodId",
                table: "FoodDistributions",
                column: "FoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodDistributions");

            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Foods");
        }
    }
}
