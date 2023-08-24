using CongestionTaxCalculator.Domain.Enums;

namespace CongestionTaxCalculator.Domain
{
    public class Motorcycle : Vehicle
    {
        public string GetVehicleType()
        {
            return VehicleTypes.Motorcycle.ToString();
        }
    }
}