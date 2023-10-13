using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudApplication.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDisabledProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "NewCategoryTable");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "NewCategoryTable",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
