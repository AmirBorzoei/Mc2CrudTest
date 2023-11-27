using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mc2.CrudTest.Repository.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCustomerTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.EnsureSchema(
                name: "CrudTest");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer",
                newSchema: "CrudTest");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Firstname_Lastname_DateOfBirth",
                schema: "CrudTest",
                table: "Customer",
                newName: "IX_Customer_Firstname_Lastname_DateOfBirth");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Email",
                schema: "CrudTest",
                table: "Customer",
                newName: "IX_Customer_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                schema: "CrudTest",
                table: "Customer",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                schema: "CrudTest",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "Customer",
                schema: "CrudTest",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_Firstname_Lastname_DateOfBirth",
                table: "Customers",
                newName: "IX_Customers_Firstname_Lastname_DateOfBirth");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_Email",
                table: "Customers",
                newName: "IX_Customers_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");
        }
    }
}
