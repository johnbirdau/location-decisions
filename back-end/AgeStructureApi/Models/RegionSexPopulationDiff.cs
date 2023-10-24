namespace AgeStructureApi.Models;

public class RegionSexPopulationDiff
{
    public int RegionCode { get; set; }

    public string RegionName { get; set; }

    public string CensusYear { get; set; }

    public List<AgePopulationDiff> Data { get; set; }
}
