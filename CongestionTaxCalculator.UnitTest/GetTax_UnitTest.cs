using CongestionTaxCalculator.Application;
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
            GothenburgCongestionTaxCalculator calculator = new();
            VehicleFactory vehicleFactory = new VehicleFactory();

            calculator.GetTax(vehicleFactory.CreateVehicle(vehicleType),movements).ShouldBeEquivalentTo(expectedTax);
        }
    }
}