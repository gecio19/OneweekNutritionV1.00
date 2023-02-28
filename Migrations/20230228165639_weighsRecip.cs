using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneweekNutrition.Migrations
{
    public partial class weighsRecip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "RecipComponent",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "RecipComponent");
        }
    }
}
