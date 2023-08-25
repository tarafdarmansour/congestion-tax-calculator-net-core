using CongestionTaxCalculator.Domain.Enums;

namespace CongestionTaxCalculator.Domain
{
    public class Military : Vehicle
    {
        public string GetVehicleType()
        {
            return VehicleTypes.Military.ToString();
        }
    }
}