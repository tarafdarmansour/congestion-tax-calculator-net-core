using CongestionTaxCalculator.Domain.Enums;

namespace CongestionTaxCalculator.Domain
{
    public class Bus : Vehicle
    {
        public string GetVehicleType()
        {
            return VehicleTypes.Bus.ToString();
        }
    }
}