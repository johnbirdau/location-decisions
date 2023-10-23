using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AgeStructureDb;

public class AgeStructureDbContext : DbContext
{
    public DbSet<FactPopulation> FactPopulations { get; set; }
    public DbSet<DimSex> DimSexes { get; set; }
    public DbSet<DimAge> DimAges { get; set; }
    public DbSet<DimState> DimStates {get;set;}
    public DbSet<DimRegionType> DimRegionTypes { get; set; }
    public DbSet<DimRegion> DimRegions { get; set; }
    public DbSet<DimYear> DimYears {get;set;}

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=tcp:172.23.64.1;Database=LocationDecisions;User Id=AgeStructureUser;Password=AgeStructureUser11;TrustServerCertificate=True;");
}

public class FactPopulation
{
    public int Id { get; set; }

    public int SexId { get; set; }
    public DimSex Sex { get; set; }

    public string AgeId { get; set; }
    public DimAge Age { get; set; }

    public int StateId { get; set; }
    public DimState State { get; set; }

    public string RegionTypeId { get; set; }
    public DimRegionType RegionType { get; set; }

    public int RegionId { get; set; }
    public DimRegion Region { get; set; }

    public int YearId { get; set; }
    public DimYear Year { get; set; }

    public int Value { get; set; }

    // public string FlagCodes { get; set; }
    // public string Flags { get; set; }
}


public class DimSex
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    
    public string Sex { get; set; }

    public List<FactPopulation> FactPopulations { get; } = new();
}

public class DimAge
{
    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string Id { get; set; }

    [StringLength(20)]
    public string Age { get; set; }

    public List<FactPopulation> FactPopulations { get; } = new();
}

public class DimState
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    
    public string State { get; set; }

    public List<FactPopulation> FactPopulations { get; } = new();
}

public class DimRegionType
{
    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string Id { get; set; }
    
    public string GeographyLevel { get; set; }

    public List<FactPopulation> FactPopulations { get; } = new();
}

public class DimRegion
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    
    public string Region { get; set; }

    public List<FactPopulation> FactPopulations { get; } = new();
}

public class DimYear
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    
    public string CensusYear { get; set; }

    public List<FactPopulation> FactPopulations { get; } = new();
}

