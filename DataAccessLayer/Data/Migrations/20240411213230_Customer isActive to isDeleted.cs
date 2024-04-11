using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CustomerisActivetoisDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Customers",
                newName: "isDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "Customers",
                newName: "isActive");
        }
    }
}
