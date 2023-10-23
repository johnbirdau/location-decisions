using System.Globalization;
using System.Transactions;
using AgeStructureDb;
using CsvHelper;
using DbCreateLoad;
using Microsoft.EntityFrameworkCore;

List<CsvRow> records;

using (var reader = new StreamReader("./ABS_C16_T01_TS_SA_08062021164508583.xls"))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    csv.Context.RegisterClassMap<CsvRowMap>();
    records = csv.GetRecords<CsvRow>().ToList();
}

using (var db = new AgeStructureDbContext())
{
    await PopulateDimSex(db, records);
    await PopulateDimAge(db, records);
    await PopulateDimState(db, records);
    await PopulateDimRegionType(db, records);
    await PopulateDimRegion(db, records);
    await PopulateDimYear(db, records);
    await PopulateFacts(db, records);
}

static async Task PopulateDimSex(AgeStructureDbContext db, List<CsvRow> records)
{
    // using var transaction = db.Database.BeginTransaction();

    var existingIds = await db.DimSexes.Select(r => r.Id).ToListAsync();

    var dimsToAdd =
        records.DistinctBy(r => r.SexId).Select(r => new DimSex() { Id = r.SexId, Sex = r.Sex })
        .Where(r => !existingIds.Contains(r.Id));

    await db.DimSexes.AddRangeAsync(dimsToAdd);
    await db.SaveChangesAsync();
    // transaction.Commit();
}

static async Task PopulateDimAge(AgeStructureDbContext db, List<CsvRow> records)
{
    // using var transaction = db.Database.BeginTransaction();

    var existingIds = await db.DimAges.Select(r => r.Id).ToListAsync();

    var dimsToAdd =
        records.DistinctBy(r => r.AgeId).Select(r => new DimAge() { Id = r.AgeId, Age = r.Age })
        .Where(r => !existingIds.Contains(r.Id));

    await db.DimAges.AddRangeAsync(dimsToAdd);
    await db.SaveChangesAsync();
    // transaction.Commit();
}

static async Task PopulateDimState(AgeStructureDbContext db, List<CsvRow> records)
{
    // using var transaction = db.Database.BeginTransaction();

    var existingIds = await db.DimStates.Select(r => r.Id).ToListAsync();

    var dimsToAdd =
        records.DistinctBy(r => r.StateId).Select(r => new DimState() { Id = r.StateId, State = r.State })
        .Where(r => !existingIds.Contains(r.Id));

    await db.DimStates.AddRangeAsync(dimsToAdd);
    await db.SaveChangesAsync();
    // transaction.Commit();
}

static async Task PopulateDimRegionType(AgeStructureDbContext db, List<CsvRow> records)
{
    // using var transaction = db.Database.BeginTransaction();

    var existingIds = await db.DimRegionTypes.Select(r => r.Id).ToListAsync();

    var dimsToAdd =
        records.DistinctBy(r => r.RegionTypeId).Select(r => new DimRegionType { Id = r.RegionTypeId, GeographyLevel = r.GeographyLevel })
        .Where(r => !existingIds.Contains(r.Id));

    await db.DimRegionTypes.AddRangeAsync(dimsToAdd);
    await db.SaveChangesAsync();
    // transaction.Commit();
}

static async Task PopulateDimRegion(AgeStructureDbContext db, List<CsvRow> records)
{
    // using var transaction = db.Database.BeginTransaction();

    var existingIds = await db.DimRegions.Select(r => r.Id).ToListAsync();

    var dimsToAdd =
        records.DistinctBy(r => r.RegionId).Select(r => new DimRegion { Id = r.RegionId, Region = r.Region })
        .Where(r => !existingIds.Contains(r.Id));

    await db.DimRegions.AddRangeAsync(dimsToAdd);
    await db.SaveChangesAsync();
    // transaction.Commit();
}

static async Task PopulateDimYear(AgeStructureDbContext db, List<CsvRow> records)
{
    // using var transaction = db.Database.BeginTransaction();

    var existingIds = await db.DimYears.Select(r => r.Id).ToListAsync();

    var dimsToAdd =
        records.DistinctBy(r => r.YearId).Select(r => new DimYear() { Id = r.YearId, CensusYear = r.CensusYear })
        .Where(r => !existingIds.Contains(r.Id));

    await db.DimYears.AddRangeAsync(dimsToAdd);
    await db.SaveChangesAsync();
    // transaction.Commit();
}

static async Task PopulateFacts(AgeStructureDbContext db, List<CsvRow> records)
{
    var factsToAdd =
        records
        .Select(r =>
            new FactPopulation
            {
                SexId = r.SexId,
                AgeId = r.AgeId,
                StateId = r.StateId,
                RegionTypeId = r.RegionTypeId,
                RegionId = r.RegionId,
                YearId = r.YearId,
                Value = r.Value
            }
        );

    await db.FactPopulations.AddRangeAsync(factsToAdd);
    await db.SaveChangesAsync();
}