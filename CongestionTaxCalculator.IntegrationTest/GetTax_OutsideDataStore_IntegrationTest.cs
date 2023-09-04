using CongestionTaxCalculator.Application;
using CongestionTaxCalculator.Domain.Services;
using CongestionTaxCalculator.Infra;
using CongestionTaxCalculator.IntegrationTest.TestData;
using Shouldly;

namespace CongestionTaxCalculator.IntegrationTest;

public class GetTax_OutsideDataStore_IntegrationTest
{
    [Theory]
    [MemberData(nameof(TestDataReader.GetGothenburgTestData), MemberType = typeof(TestDataReader))]
    public void IntegrationTest_GetTax_UnitTest(string vehicleType, DateTime[] movements, int expectedTax)
    {
        IRuleService ruleService = new CallingOutsideDataStoreService();
        ITaxService taxService = new TaxService(ruleService);
        GothenburgCongestionTaxCalculator calculator = new(taxService);

        calculator.GetTotalTax(new CalculateCarTaxRequestDto(vehicleType, movements)).GetAwaiter().GetResult().ShouldBeEquivalentTo(expectedTax);
    }
}