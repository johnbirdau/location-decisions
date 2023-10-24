using AgeStructureApi.Models;
using AgeStructureDb;
using Microsoft.AspNetCore.Mvc;

namespace AgeStructureApi.Controllers;

[ApiController]
[Route("api/age-structure")]
public class AgeStructureController : ControllerBase
{
    //private readonly ILogger<AgeStructureController> _logger;

    // public AgeStructureController(ILogger<AgeStructureController> logger)
    // {
    //     _logger = logger;
    // }

    [HttpGet("{regionId}/{sexId}")]
    public RegionSexPopulation Get(
        int regionId,
        int sexId
    )
    {
        using var db = new AgeStructureDbContext();
        var data =
            from f in db.FactPopulations
            where f.SexId == sexId && f.RegionId == regionId
            select
                new AgePopulation
                {
                    Age = f.Age.Age,
                    Sex = f.Sex.Sex,
                    CensusYear = f.YearId,
                    Population = f.Value
                };

        var regionName =
            from r in db.DimRegions
            where r.Id == regionId
            select r.Region;

        return new RegionSexPopulation
        {
            RegionCode = regionId,
            RegionName = regionName.FirstOrDefault(),
            Data = data.ToList()
        }; ;
    }

    [HttpGet("state/{stateId}/{sexId}")]
    public StateSexPopulation GetByState(
        int stateId,
        int sexId
    )
    {
        using var db = new AgeStructureDbContext();
        var data =
            from f in db.FactPopulations
            where f.SexId == sexId && f.StateId == stateId
            group f by new { f.AgeId, f.YearId } into g
            select
                new AgePopulation
                {
                    Age = g.First().Age.Age,
                    Sex = g.First().Sex.Sex,
                    CensusYear = g.Key.YearId,
                    Population = g.Sum(f => f.Value)
                };

        var stateName =
            from r in db.DimStates
            where r.Id == stateId
            select r.State;

        return new StateSexPopulation
        {
            StateCode = stateId,
            StateName = stateName.FirstOrDefault(),
            Data = data.OrderBy(d => d.CensusYear).ThenBy(d => d.Age).ThenBy(d => d.Sex).ToList()
        }; ;
    }
}
