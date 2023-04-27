using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L00177784_Project.Migrations
{
    /// <inheritdoc />
    public partial class AddSalesFieldProcessed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Processed",
                table: "Sales",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Processed",
                table: "Sales");
        }
    }
}
