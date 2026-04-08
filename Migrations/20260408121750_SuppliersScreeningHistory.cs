using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DueDiligenceChecker.Migrations
{
    /// <inheritdoc />
    public partial class SuppliersScreeningHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "supplier_screenings",
                schema: "suppliers",
                columns: table => new
                {
                    supplier_screening_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    supplier_id = table.Column<int>(type: "int", nullable: false),
                    executed_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    has_hits = table.Column<bool>(type: "bit", nullable: false),
                    sources_checked = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_supplier_screenings", x => x.supplier_screening_id);
                    table.ForeignKey(
                        name: "fk_supplier_screenings_suppliers_supplier_id",
                        column: x => x.supplier_id,
                        principalSchema: "suppliers",
                        principalTable: "suppliers",
                        principalColumn: "supplier_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interpol_screening_hits",
                schema: "suppliers",
                columns: table => new
                {
                    interpol_screening_hit_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    family_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    forename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: true),
                    place_of_birth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    charges = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supplier_screening_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_interpol_screening_hits", x => x.interpol_screening_hit_id);
                    table.ForeignKey(
                        name: "fk_interpol_screening_hits_supplier_screenings_supplier_screening_id",
                        column: x => x.supplier_screening_id,
                        principalSchema: "suppliers",
                        principalTable: "supplier_screenings",
                        principalColumn: "supplier_screening_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "secop_screening_hits",
                schema: "suppliers",
                columns: table => new
                {
                    secop_screening_hit_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    entity_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    entity_tax_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    municipality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    resolution_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contractor_document = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contractor_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contract_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sanction_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    published_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    finalized_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    loaded_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    process_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supplier_screening_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_secop_screening_hits", x => x.secop_screening_hit_id);
                    table.ForeignKey(
                        name: "fk_secop_screening_hits_supplier_screenings_supplier_screening_id",
                        column: x => x.supplier_screening_id,
                        principalSchema: "suppliers",
                        principalTable: "supplier_screenings",
                        principalColumn: "supplier_screening_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "smv_screening_hits",
                schema: "suppliers",
                columns: table => new
                {
                    smv_screening_hit_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    resolution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    with_appeal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    resolutive_resolution_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    resolutive_resolution_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supplier_screening_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_smv_screening_hits", x => x.smv_screening_hit_id);
                    table.ForeignKey(
                        name: "fk_smv_screening_hits_supplier_screenings_supplier_screening_id",
                        column: x => x.supplier_screening_id,
                        principalSchema: "suppliers",
                        principalTable: "supplier_screenings",
                        principalColumn: "supplier_screening_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_interpol_screening_hits_supplier_screening_id",
                schema: "suppliers",
                table: "interpol_screening_hits",
                column: "supplier_screening_id");

            migrationBuilder.CreateIndex(
                name: "ix_secop_screening_hits_supplier_screening_id",
                schema: "suppliers",
                table: "secop_screening_hits",
                column: "supplier_screening_id");

            migrationBuilder.CreateIndex(
                name: "ix_smv_screening_hits_supplier_screening_id",
                schema: "suppliers",
                table: "smv_screening_hits",
                column: "supplier_screening_id");

            migrationBuilder.CreateIndex(
                name: "ix_supplier_screenings_supplier_id",
                schema: "suppliers",
                table: "supplier_screenings",
                column: "supplier_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "interpol_screening_hits",
                schema: "suppliers");

            migrationBuilder.DropTable(
                name: "secop_screening_hits",
                schema: "suppliers");

            migrationBuilder.DropTable(
                name: "smv_screening_hits",
                schema: "suppliers");

            migrationBuilder.DropTable(
                name: "supplier_screenings",
                schema: "suppliers");
        }
    }
}
