using AgeStructureApi.Models;
using AgeStructureDb;
using Microsoft.AspNetCore.Mvc;

namespace AgeStructureApi.Controllers;

[ApiController]
[Route("api/age-structure-diff")]
public class AgeStructureDiffController : ControllerBase
{
    private readonly ILogger<AgeStructureDiffController> _logger;

    public AgeStructureDiffController(ILogger<AgeStructureDiffController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{regionId}/{sexId}/{year1}/{year2}")]
    public RegionSexPopulationDiff Get(
        int regionId,
        int sexId,
        int year1,
        int year2
    )
    {
        using var db = new AgeStructureDbContext();
        var year1Data = QueryYearData(regionId, sexId, year1, db);
        var year2Data = QueryYearData(regionId, sexId, year2, db);

        // would be more efficient to diff in DB, but in the interests of assignemnt time...
        // currently assumes same data points for both years are available.
        var data =
            year1Data
            .Zip(
                year2Data,
                (y1, y2) =>
                    new AgePopulationDiff
                    {
                        Age = y1.Age,
                        Sex = y1.Sex,
                        Population = y2.Population - y1.Population
                    }
            );

        var regionName =
            from r in db.DimRegions
            where r.Id == regionId
            select r.Region;

        return new RegionSexPopulationDiff
        {
            RegionCode = regionId,
            RegionName = regionName.FirstOrDefault(),
            CensusYear = $"{year1}-{year2}",
            Data = data.ToList()
        }; ;
    }

    private static List<AgePopulationDiff> QueryYearData(int regionId, int sexId, int year1, AgeStructureDbContext db) =>
        (
            from f1 in db.FactPopulations
            where f1.SexId == sexId && f1.RegionId == regionId && f1.YearId == year1
            orderby f1.AgeId
            select
                new AgePopulationDiff
                {
                    Age = f1.Age.Age,
                    Sex = f1.Sex.Sex,
                    Population = f1.Value
                }
        ).ToList();

    // [HttpGet("state/{stateId}/{sexId}")]
    // public StateSexPopulation GetByState(
    //     int stateId,
    //     int sexId
    // )
    // {
    //     using var db = new AgeStructureDbContext();
    //     var data =
    //         from f in db.FactPopulations
    //         where f.SexId == sexId && f.StateId == stateId
    //         group f by new { f.AgeId, f.YearId } into g
    //         select 
    //             new AgePopulation
    //             {
    //                 Age = g.First().Age.Age,
    //                 Sex = g.First().Sex.Sex,
    //                 CensusYear = g.Key.YearId,
    //                 Population = g.Sum(f => f.Value)
    //             };

    //     var stateName =
    //         from r in db.DimStates
    //         where r.Id == stateId
    //         select r.State;

    //     return new StateSexPopulation{
    //             StateCode = stateId,
    //             StateName = stateName.FirstOrDefault(),
    //             Data = data.ToList()
    //         };;
    // }
}
