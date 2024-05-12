using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class seedrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0442066a-2d28-426d-a056-005b04f08f57", null, "Employee", "EMPLOYEE" },
                    { "9a01a228-8641-4bbe-b0e0-dba3fa664ff7", null, "HumanResource", "HUMANRESOURCE" },
                    { "b4056d3e-210b-4fb6-b181-592c38c58bcf", null, "SuperUser", "SUPERUSER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0442066a-2d28-426d-a056-005b04f08f57");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a01a228-8641-4bbe-b0e0-dba3fa664ff7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4056d3e-210b-4fb6-b181-592c38c58bcf");
        }
    }
}
