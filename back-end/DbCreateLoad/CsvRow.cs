using CsvHelper.Configuration;

namespace DbCreateLoad
{
    public class CsvRow 
    {
        public int SexId { get; set; }
        public string Sex { get; set; }
        public string AgeId { get; set; }
        public string Age { get; set; }
        public int StateId { get; set; }
        public string State { get; set; }
        public string RegionTypeId { get; set; }
        public string GeographyLevel { get; set; }
        public int RegionId { get; set; }
        public string Region { get; set; }
        public int YearId { get; set; }
        public string CensusYear { get; set; }
        public int Value { get; set; }
    }

    public sealed class CsvRowMap : ClassMap<CsvRow>
{
    public CsvRowMap()
    {
        Map(m => m.SexId).Name("SEX_ABS");
        Map(m => m.Sex).Name("Sex");
        Map(m => m.AgeId).Name("AGE");
        Map(m => m.Age).Name("Age");
        Map(m => m.StateId).Name("STATE");
        Map(m => m.State).Name("State");
        Map(m => m.RegionTypeId).Name("REGIONTYPE");
        Map(m => m.GeographyLevel).Name("Geography Level");
        Map(m => m.RegionId).Name("ASGS_2016");
        Map(m => m.Region).Name("Region");
        Map(m => m.YearId).Name("TIME");
        Map(m => m.CensusYear).Name("Census year");
        Map(m => m.Value).Name("Value");
    }
}
}