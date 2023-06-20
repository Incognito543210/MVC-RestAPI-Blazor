using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class database_relation_many_to_many_test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HasIngridients",
                table: "HasIngridients");

            migrationBuilder.DropIndex(
                name: "IX_HasIngridients_RecipeID",
                table: "HasIngridients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HasCategories",
                table: "HasCategories");

            migrationBuilder.DropIndex(
                name: "IX_HasCategories_RecipeID",
                table: "HasCategories");

            migrationBuilder.AlterColumn<int>(
                name: "HasIngridientID",
                table: "HasIngridients",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "HasCategoryID",
                table: "HasCategories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HasIngridients",
                table: "HasIngridients",
                columns: new[] { "RecipeID", "IngridientID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_HasCategories",
                table: "HasCategories",
                columns: new[] { "RecipeID", "TagID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HasIngridients",
                table: "HasIngridients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HasCategories",
                table: "HasCategories");

            migrationBuilder.AlterColumn<int>(
                name: "HasIngridientID",
                table: "HasIngridients",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "HasCategoryID",
                table: "HasCategories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HasIngridients",
                table: "HasIngridients",
                column: "HasIngridientID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HasCategories",
                table: "HasCategories",
                column: "HasCategoryID");

            migrationBuilder.CreateTable(
                name: "RecipeTags",
                columns: table => new
                {
                    RecipeTagID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeID = table.Column<int>(type: "INTEGER", nullable: false),
                    TagID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTags", x => x.RecipeTagID);
                    table.ForeignKey(
                        name: "FK_RecipeTags_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeTags_Tags_TagID",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HasIngridients_RecipeID",
                table: "HasIngridients",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_HasCategories_RecipeID",
                table: "HasCategories",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTags_RecipeID",
                table: "RecipeTags",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTags_TagID",
                table: "RecipeTags",
                column: "TagID");
        }
    }
}
