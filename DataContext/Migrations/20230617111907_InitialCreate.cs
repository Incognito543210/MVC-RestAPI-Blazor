using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingridients",
                columns: table => new
                {
                    IngridientID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingridients", x => x.IngridientID);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    PrepareTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PostData = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Portions = table.Column<int>(type: "INTEGER", nullable: false),
                    Difficulty = table.Column<int>(type: "INTEGER", nullable: false),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeID);
                    table.ForeignKey(
                        name: "FK_Recipes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HasCategories",
                columns: table => new
                {
                    HasCategoryID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeID = table.Column<int>(type: "INTEGER", nullable: false),
                    IngridientID = table.Column<int>(type: "INTEGER", nullable: false),
                    TagID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HasCategories", x => x.HasCategoryID);
                    table.ForeignKey(
                        name: "FK_HasCategories_Ingridients_IngridientID",
                        column: x => x.IngridientID,
                        principalTable: "Ingridients",
                        principalColumn: "IngridientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HasCategories_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HasCategories_Tags_TagID",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "TagID");
                });

            migrationBuilder.CreateTable(
                name: "HasIngridients",
                columns: table => new
                {
                    HasIngridientID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amonut = table.Column<string>(type: "TEXT", nullable: false),
                    RecipeID = table.Column<int>(type: "INTEGER", nullable: false),
                    IngridientID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HasIngridients", x => x.HasIngridientID);
                    table.ForeignKey(
                        name: "FK_HasIngridients_Ingridients_IngridientID",
                        column: x => x.IngridientID,
                        principalTable: "Ingridients",
                        principalColumn: "IngridientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HasIngridients_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "Opinions",
                columns: table => new
                {
                    OpinionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PostData = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    Rate = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipeID = table.Column<int>(type: "INTEGER", nullable: false),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opinions", x => x.OpinionID);
                    table.ForeignKey(
                        name: "FK_Opinions_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Opinions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
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
                name: "IX_HasCategories_IngridientID",
                table: "HasCategories",
                column: "IngridientID");

            migrationBuilder.CreateIndex(
                name: "IX_HasCategories_RecipeID",
                table: "HasCategories",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_HasCategories_TagID",
                table: "HasCategories",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_HasIngridients_IngridientID",
                table: "HasIngridients",
                column: "IngridientID");

            migrationBuilder.CreateIndex(
                name: "IX_HasIngridients_RecipeID",
                table: "HasIngridients",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_IngridientRecipe_RecipesRecipeID",
                table: "IngridientRecipe",
                column: "RecipesRecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Opinions_RecipeID",
                table: "Opinions",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Opinions_UserID",
                table: "Opinions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserID",
                table: "Recipes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTag_tagsTagID",
                table: "RecipeTag",
                column: "tagsTagID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HasCategories");

            migrationBuilder.DropTable(
                name: "HasIngridients");

            migrationBuilder.DropTable(
                name: "IngridientRecipe");

            migrationBuilder.DropTable(
                name: "Opinions");

            migrationBuilder.DropTable(
                name: "RecipeTag");

            migrationBuilder.DropTable(
                name: "Ingridients");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
