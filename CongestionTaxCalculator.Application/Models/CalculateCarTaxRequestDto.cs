using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.Application;

public record CalculateCarTaxRequestDto
{
    public CalculateCarTaxRequestDto(string vehicleType, DateTime[] movements)
    {
        Movements = movements;
        Vehicle = new VehicleFactory().CreateVehicle(vehicleType);
    }

    public Vehicle Vehicle { get; }
    public DateTime[] Movements { get; }
}