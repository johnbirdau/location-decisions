using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgeStructureDb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DimAges",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Age = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimAges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DimRegions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimRegions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DimRegionTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    GeographyLevel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimRegionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DimSexes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimSexes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DimStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DimYears",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CensusYear = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FactPopulations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SexId = table.Column<int>(type: "int", nullable: false),
                    AgeId = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    RegionTypeId = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    YearId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactPopulations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactPopulations_DimAges_AgeId",
                        column: x => x.AgeId,
                        principalTable: "DimAges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactPopulations_DimRegionTypes_RegionTypeId",
                        column: x => x.RegionTypeId,
                        principalTable: "DimRegionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactPopulations_DimRegions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "DimRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactPopulations_DimSexes_SexId",
                        column: x => x.SexId,
                        principalTable: "DimSexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactPopulations_DimStates_StateId",
                        column: x => x.StateId,
                        principalTable: "DimStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactPopulations_DimYears_YearId",
                        column: x => x.YearId,
                        principalTable: "DimYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FactPopulations_AgeId",
                table: "FactPopulations",
                column: "AgeId");

            migrationBuilder.CreateIndex(
                name: "IX_FactPopulations_RegionId",
                table: "FactPopulations",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_FactPopulations_RegionTypeId",
                table: "FactPopulations",
                column: "RegionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FactPopulations_SexId",
                table: "FactPopulations",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_FactPopulations_StateId",
                table: "FactPopulations",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_FactPopulations_YearId",
                table: "FactPopulations",
                column: "YearId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FactPopulations");

            migrationBuilder.DropTable(
                name: "DimAges");

            migrationBuilder.DropTable(
                name: "DimRegionTypes");

            migrationBuilder.DropTable(
                name: "DimRegions");

            migrationBuilder.DropTable(
                name: "DimSexes");

            migrationBuilder.DropTable(
                name: "DimStates");

            migrationBuilder.DropTable(
                name: "DimYears");
        }
    }
}
