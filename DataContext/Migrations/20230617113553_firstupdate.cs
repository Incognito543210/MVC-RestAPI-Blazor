using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class firstupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HasCategories_Ingridients_IngridientID",
                table: "HasCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_HasCategories_Tags_TagID",
                table: "HasCategories");

            migrationBuilder.DropIndex(
                name: "IX_HasCategories_IngridientID",
                table: "HasCategories");

            migrationBuilder.DropColumn(
                name: "IngridientID",
                table: "HasCategories");

            migrationBuilder.AlterColumn<int>(
                name: "TagID",
                table: "HasCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HasCategories_Tags_TagID",
                table: "HasCategories",
                column: "TagID",
                principalTable: "Tags",
                principalColumn: "TagID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HasCategories_Tags_TagID",
                table: "HasCategories");

            migrationBuilder.AlterColumn<int>(
                name: "TagID",
                table: "HasCategories",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "IngridientID",
                table: "HasCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HasCategories_IngridientID",
                table: "HasCategories",
                column: "IngridientID");

            migrationBuilder.AddForeignKey(
                name: "FK_HasCategories_Ingridients_IngridientID",
                table: "HasCategories",
                column: "IngridientID",
                principalTable: "Ingridients",
                principalColumn: "IngridientID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HasCategories_Tags_TagID",
                table: "HasCategories",
                column: "TagID",
                principalTable: "Tags",
                principalColumn: "TagID");
        }
    }
}
