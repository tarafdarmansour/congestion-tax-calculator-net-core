using CongestionTaxCalculator.UnitTest.Models;
using Newtonsoft.Json.Linq;

namespace CongestionTaxCalculator.UnitTest.TestData;

internal class TestDataReader
{
    public static List<object[]> GetGothenburgTestData()
    {
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData\\Gothenburg-TestData.json");
        var json = File.ReadAllText(filePath);
        var jobject = JObject.Parse(json);
        var expectedTaxData = jobject["TestDat"]?.ToObject<IEnumerable<VehicleMovementTestModel>>();

        var movementsAndTax = new List<object[]>();
        foreach (var node in expectedTaxData)
            movementsAndTax.Add(new object[] { node.VehicleType, node.Movements, node.ExpectedTax });
        return movementsAndTax.ToList();
    }
}