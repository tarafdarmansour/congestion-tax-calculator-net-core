using CongestionTaxCalculator.Domain.Enums;

namespace CongestionTaxCalculator.Domain
{
    public class Diplomat : Vehicle
    {
        public string GetVehicleType()
        {
            return VehicleTypes.Diplomat.ToString();
        }
    }
}