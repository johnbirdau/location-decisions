namespace AgeStructureApi.Models;

public class RegionSexPopulation
{
    public int RegionCode { get; set; }

    public string RegionName { get; set; }

    public List<AgePopulation> Data { get; set; }
}
