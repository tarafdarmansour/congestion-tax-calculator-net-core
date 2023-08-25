using CongestionTaxCalculator.Domain.Enums;

namespace CongestionTaxCalculator.Domain
{
    public class Emergency : Vehicle
    {
        public string GetVehicleType()
        {
            return VehicleTypes.Emergency.ToString();
        }
    }
}