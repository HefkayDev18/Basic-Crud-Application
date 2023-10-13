using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudApplication.Migrations
{
    /// <inheritdoc />
    public partial class FilterEntityModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_NewCategoryTable_CategoryTableId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryTableId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_NewCategoryTable_CategoryTableId",
                table: "Employees",
                column: "CategoryTableId",
                principalTable: "NewCategoryTable",
                principalColumn: "CategoryTableId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_NewCategoryTable_CategoryTableId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryTableId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_NewCategoryTable_CategoryTableId",
                table: "Employees",
                column: "CategoryTableId",
                principalTable: "NewCategoryTable",
                principalColumn: "CategoryTableId");
        }
    }
}
