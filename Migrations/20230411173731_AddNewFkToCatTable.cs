using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFkToCatTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryTableId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NewCategoryTable",
                columns: table => new
                {
                    CategoryTableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewCategoryTable", x => x.CategoryTableId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CategoryTableId",
                table: "Employees",
                column: "CategoryTableId");

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

            migrationBuilder.DropTable(
                name: "NewCategoryTable");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CategoryTableId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CategoryTableId",
                table: "Employees");
        }
    }
}
