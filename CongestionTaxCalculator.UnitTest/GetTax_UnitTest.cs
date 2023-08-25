using CongestionTaxCalculator.Application;
using CongestionTaxCalculator.Domain.Services;
using CongestionTaxCalculator.Infra;
using CongestionTaxCalculator.Infra.DatabaseContext;
using CongestionTaxCalculator.Infra.Repositories;
using CongestionTaxCalculator.UnitTest.Factories;
using CongestionTaxCalculator.UnitTest.TestData;
using Shouldly;

namespace CongestionTaxCalculator.UnitTest
{
    public class GetTax_UnitTest
    {
        [Theory]
        [MemberData(nameof(TestDataReader.GetGothenburgTestData), MemberType = typeof(TestDataReader))]
        public void Gothenburg_GetTax_UnitTest(string vehicleType, DateTime[] movements,int expectedTax)
        {
            AppDbContext context = new AppDbContext();
            GothenburgRuleRepository repository = new GothenburgRuleRepository(context);
            RuleService ruleService = new RuleService(repository);
            ITaxService taxService = new TaxService(ruleService);
            GothenburgCongestionTaxCalculator calculator = new(taxService);
            VehicleFactory vehicleFactory = new VehicleFactory();

            calculator.GetTotalTax(vehicleFactory.CreateVehicle(vehicleType),movements).ShouldBeEquivalentTo(expectedTax);
        }
    }
}