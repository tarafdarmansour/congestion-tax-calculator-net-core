using CongestionTaxCalculator.IntegrationTest.Models;
using Newtonsoft.Json.Linq;

namespace CongestionTaxCalculator.IntegrationTest.TestData;

internal class TestDataReader
{
    public static List<object[]> GetGothenburgTestData()
    {
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData\\Gothenburg-TestData.json");
        var json = File.ReadAllText(filePath);
        var jobject = JObject.Parse(json);
        var expectedTaxData = jobject["TestData"]?.ToObject<IEnumerable<VehicleMovementValidDataModel>>();

        var movementsAndTax = new List<object[]>();
        foreach (var node in expectedTaxData)
            movementsAndTax.Add(new object[] { node.VehicleType, node.Movements, node.ExpectedTax });
        return movementsAndTax.ToList();
    }
    
}