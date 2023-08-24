using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Enums;

namespace CongestionTaxCalculator.UnitTest.Factories;

public class VehicleFactory
{
    private readonly Dictionary<string, Func<Vehicle>> _map;

    public VehicleFactory()
    {
        _map = new Dictionary<string, Func<Vehicle>>
        {
            { VehicleTypes.Car.ToString(), () => new Car() },
            { VehicleTypes.Motorcycle.ToString(), () => new Motorcycle() },
            { VehicleTypes.Tractor.ToString(), () => new Tractor() },
            { VehicleTypes.Emergency.ToString(), () => new Emergency() },
            { VehicleTypes.Diplomat.ToString(), () => new Diplomat() },
            { VehicleTypes.Foreign.ToString(), () => new Foreign() },
            { VehicleTypes.Military.ToString(), () => new Military() },
            { VehicleTypes.Bus.ToString(), () => new Bus() },
        };
    }

    public Vehicle CreateVehicle(string name)
    {
        return _map.ContainsKey(name)
            ? _map[name]()
            : new NotExistVehicle();
    }
}