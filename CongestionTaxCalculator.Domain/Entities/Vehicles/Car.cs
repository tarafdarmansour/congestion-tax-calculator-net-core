using CongestionTaxCalculator.Domain.Enums;

namespace CongestionTaxCalculator.Domain
{
    public class Car : Vehicle
    {
        public string GetVehicleType()
        {
            return VehicleTypes.Car.ToString();
        }
    }
}