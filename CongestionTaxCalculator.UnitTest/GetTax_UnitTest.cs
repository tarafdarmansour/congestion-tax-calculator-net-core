using CongestionTaxCalculator.Application;
using CongestionTaxCalculator.Domain.Services;
using CongestionTaxCalculator.Infra;
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
            ITaxService taxService = new TaxService();
            GothenburgCongestionTaxCalculator calculator = new(taxService);
            VehicleFactory vehicleFactory = new VehicleFactory();

            calculator.GetTotalTax(vehicleFactory.CreateVehicle(vehicleType),movements).ShouldBeEquivalentTo(expectedTax);
        }
    }
}