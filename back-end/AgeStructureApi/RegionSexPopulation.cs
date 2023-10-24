namespace AgeStructureApi;

public class RegionSexPopulation
{
    public int RegionCode { get; set; }

    public string? RegionName { get; set; }

    public List<AgePopulation> Data { get; set; }
}

public class StateSexPopulation
{
    public int StateCode { get; set; }

    public string? StateName { get; set; }

    public List<AgePopulation> Data { get; set; }
}

public class AgePopulation
{
    public string Age { get; set; }
    public string Sex { get; set; }
    public int CensusYear { get; set; }
    public int Population { get; set; }
}