using CongestionTaxCalculator.Domain.Enums;

namespace CongestionTaxCalculator.Domain
{
    public class Tractor : Vehicle
    {
        public string GetVehicleType()
        {
            return VehicleTypes.Tractor.ToString();
        }
    }
}