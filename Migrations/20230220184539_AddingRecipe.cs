using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneweekNutrition.Migrations
{
    public partial class AddingRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Component",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "Component",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Calories = table.Column<int>(type: "int", nullable: true),
                    Carbohydrates = table.Column<int>(type: "int", nullable: true),
                    Protein = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Component_RecipeId",
                table: "Component",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Component_Recipes_RecipeId",
                table: "Component",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Component_Recipes_RecipeId",
                table: "Component");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Component_RecipeId",
                table: "Component");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Component");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Component",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
