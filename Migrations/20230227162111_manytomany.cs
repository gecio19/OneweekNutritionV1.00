using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneweekNutrition.Migrations
{
    public partial class manytomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Component_Recipes_RecipeId",
                table: "Component");

            migrationBuilder.DropIndex(
                name: "IX_Component_RecipeId",
                table: "Component");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Component");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Recipes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RecipComponent",
                columns: table => new
                {
                    CompId = table.Column<int>(type: "int", nullable: false),
                    RecipId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipComponent", x => new { x.CompId, x.RecipId });
                    table.ForeignKey(
                        name: "FK_RecipComponent_Component_CompId",
                        column: x => x.CompId,
                        principalTable: "Component",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipComponent_Recipes_RecipId",
                        column: x => x.RecipId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RecipComponent_RecipId",
                table: "RecipComponent",
                column: "RecipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipComponent");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Recipes");

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "Component",
                type: "int",
                nullable: true);

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
    }
}
