namespace AgeStructureApi.Models;

public class StateSexPopulationDiff
{
    public int StateCode { get; set; }

    public string StateName { get; set; }

    public string CensusYear { get; set; }

    public List<AgePopulationDiff> Data { get; set; }
}
