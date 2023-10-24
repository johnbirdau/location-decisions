using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgeStructureDb.Migrations
{
    /// <inheritdoc />
    public partial class PopulationByAgeGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE OR ALTER VIEW dbo.PopulationByAgeGroup AS
WITH
RangeCte AS
(
	SELECT Value as RangeLow, Value + 4 As RangeHigh, CAST(Value as VARCHAR(5)) + '-' + CAST(Value+4 as VARCHAR(5)) AS RangeDisplay
	FROM GENERATE_SERIES(0, (select MAX(CAST(Age as int)) + 4 from DimAges where ISNUMERIC(Age) = 1), 5)
),
SpecificAgeFactsCte AS
(
	SELECT AgeId, Value
	FROM FactPopulations f
	Where ISNUMERIC(f.AgeId) = 1
)
SELECT r.RangeLow, r.RangeHigh, r.RangeDisplay AS Range, SUM(f.Value) AS Value
FROM RangeCte r
LEFT JOIN SpecificAgeFactsCte f ON r.RangeLow <=  CAST(f.AgeId as int) AND r.RangeHigh >= CAST(f.AgeId as int)
GROUP BY r.RangeLow, r.RangeHigh, r.RangeDisplay"

            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW dbo.PopulationByAgeGroup;");
        }
    }
}
