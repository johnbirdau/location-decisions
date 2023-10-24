namespace AgeStructureApi.Models;

public class StateSexPopulation
{
    public int StateCode { get; set; }

    public string StateName { get; set; }

    public List<AgePopulation> Data { get; set; }
}