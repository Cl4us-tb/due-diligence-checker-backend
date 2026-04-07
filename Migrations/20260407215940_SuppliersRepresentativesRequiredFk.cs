using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DueDiligenceChecker.Migrations
{
    /// <inheritdoc />
    public partial class SuppliersRepresentativesRequiredFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "supplier_id",
                schema: "suppliers",
                table: "representatives",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "supplier_id",
                schema: "suppliers",
                table: "representatives",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
