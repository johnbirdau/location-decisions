using System.Text.Json;
using AgeStructureApi.Controllers;

namespace AgeStructureApi.Tests;

public class UnitTest1
{
    [Fact]
    public void AgeStructure_GetByState_7_1_Test()
    {
        // Setup
        var controller = new AgeStructureController();
        
        // Act
        var result = controller.GetByState(7, 1);

        // Test
        Assert.Equal("Northern Territory", result.StateName);
        Assert.Equal(7, result.StateCode);
        Assert.NotNull(result.Data);

        //  234 is result row count from:
        //     SELECT AgeId, YearId, SUM(Value) as Value
        //     FROM FactPopulations
        //     WHERE stateId = '7' and sexid = 1
        //     group by YearId, AgeId, SexId
        //     order by YearId, AgeId, SexId
        Assert.Equal(234, result.Data.Count);

        // Expected results content verified against above query output.
        var expectedJsonResult = File.OpenText("ExpectedResults/AgeStructure_GetByState_7_1.json").ReadToEnd();
        Assert.Equal(
            expectedJsonResult,
            JsonSerializer.Serialize(
                result,
                new JsonSerializerOptions{
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
        ));
    }
}