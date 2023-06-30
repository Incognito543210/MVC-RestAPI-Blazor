using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class change_name_of_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Ingridients",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Amonut",
                table: "HasIngridients",
                newName: "Amount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Ingridients",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "HasIngridients",
                newName: "Amonut");
        }
    }
}
