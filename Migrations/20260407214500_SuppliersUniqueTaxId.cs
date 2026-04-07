using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DueDiligenceChecker.Migrations
{
    /// <inheritdoc />
    public partial class SuppliersUniqueTaxId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "tax_identification",
                schema: "suppliers",
                table: "suppliers",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "ix_suppliers_tax_identification",
                schema: "suppliers",
                table: "suppliers",
                column: "tax_identification",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_suppliers_tax_identification",
                schema: "suppliers",
                table: "suppliers");

            migrationBuilder.AlterColumn<string>(
                name: "tax_identification",
                schema: "suppliers",
                table: "suppliers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);
        }
    }
}
