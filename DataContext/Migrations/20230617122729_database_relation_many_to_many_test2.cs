using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class database_relation_many_to_many_test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngridientRecipe");

            migrationBuilder.DropTable(
                name: "RecipeTag");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IngridientRecipe",
                columns: table => new
                {
                    IngridientsIngridientID = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipesRecipeID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngridientRecipe", x => new { x.IngridientsIngridientID, x.RecipesRecipeID });
                    table.ForeignKey(
                        name: "FK_IngridientRecipe_Ingridients_IngridientsIngridientID",
                        column: x => x.IngridientsIngridientID,
                        principalTable: "Ingridients",
                        principalColumn: "IngridientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngridientRecipe_Recipes_RecipesRecipeID",
                        column: x => x.RecipesRecipeID,
                        principalTable: "Recipes",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeTag",
                columns: table => new
                {
                    RecipesRecipeID = table.Column<int>(type: "INTEGER", nullable: false),
                    tagsTagID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTag", x => new { x.RecipesRecipeID, x.tagsTagID });
                    table.ForeignKey(
                        name: "FK_RecipeTag_Recipes_RecipesRecipeID",
                        column: x => x.RecipesRecipeID,
                        principalTable: "Recipes",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeTag_Tags_tagsTagID",
                        column: x => x.tagsTagID,
                        principalTable: "Tags",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngridientRecipe_RecipesRecipeID",
                table: "IngridientRecipe",
                column: "RecipesRecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTag_tagsTagID",
                table: "RecipeTag",
                column: "tagsTagID");
        }
    }
}
