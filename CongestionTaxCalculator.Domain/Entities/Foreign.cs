using CongestionTaxCalculator.Domain.Enums;

namespace CongestionTaxCalculator.Domain
{
    public class Foreign : Vehicle
    {
        public string GetVehicleType()
        {
            return VehicleTypes.Foreign.ToString();
        }
    }
}