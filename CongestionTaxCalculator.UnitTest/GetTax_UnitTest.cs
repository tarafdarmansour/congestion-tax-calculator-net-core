using CongestionTaxCalculator.Application;
using CongestionTaxCalculator.Domain.Services;
using CongestionTaxCalculator.Infra;
using CongestionTaxCalculator.Infra.DatabaseContext;
using CongestionTaxCalculator.Infra.Repositories;
using CongestionTaxCalculator.Shared.Exceptions;
using CongestionTaxCalculator.UnitTest.TestData;
using Shouldly;

namespace CongestionTaxCalculator.UnitTest;

public class GetTax_UnitTest
{
    [Theory]
    [MemberData(nameof(TestDataReader.GetGothenburgTestData), MemberType = typeof(TestDataReader))]
    public void Gothenburg_GetTax_UnitTest(string vehicleType, DateTime[] movements, int expectedTax)
    {
        var context = new AppDbContext();
        var repository = new GothenburgRuleRepository(context);
        var ruleService = new RuleService(repository);
        ITaxService taxService = new TaxService(ruleService);
        GothenburgCongestionTaxCalculator calculator = new(taxService);

        calculator.GetTotalTax(new CalculateCarTaxRequestDto(vehicleType, movements)).GetAwaiter().GetResult().ShouldBeEquivalentTo(expectedTax);
    }

    [Theory]
    [MemberData(nameof(TestDataReader.GetGothenburgInvalidData), MemberType = typeof(TestDataReader))]
    public void Gothenburg_GetTax_InvalidData_UnitTest(string vehicleType, DateTime[] movements, string exception)
    {
        var context = new AppDbContext();
        var repository = new GothenburgRuleRepository(context);
        var ruleService = new RuleService(repository);
        ITaxService taxService = new TaxService(ruleService);
        GothenburgCongestionTaxCalculator calculator = new(taxService);

        Action callGetTotalTax = () => calculator.GetTotalTax(new CalculateCarTaxRequestDto(vehicleType, movements)).GetAwaiter().GetResult();

        callGetTotalTax.ShouldThrow<BaseException>();
    }
}