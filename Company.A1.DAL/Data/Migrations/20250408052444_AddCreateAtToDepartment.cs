using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.A1.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreateAtToDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsADeleted",
                table: "Employees",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Departments",
                newName: "CreateAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Employees",
                newName: "IsADeleted");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Departments",
                newName: "CreatedDate");
        }
    }
}
