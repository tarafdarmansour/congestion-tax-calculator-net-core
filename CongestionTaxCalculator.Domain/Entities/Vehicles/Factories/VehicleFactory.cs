using System;
using System.Collections.Generic;
using CongestionTaxCalculator.Domain.Enums;

namespace CongestionTaxCalculator.Domain;

public class VehicleFactory
{
    private readonly Dictionary<string, Func<Vehicle>> _map;

    public VehicleFactory()
    {
        _map = new Dictionary<string, Func<Vehicle>>
        {
            { VehicleTypes.Car.ToString().ToLower(), () => new Car() },
            { VehicleTypes.Motorcycle.ToString().ToLower(), () => new Motorcycle() },
            { VehicleTypes.Tractor.ToString().ToLower(), () => new Tractor() },
            { VehicleTypes.Emergency.ToString().ToLower(), () => new Emergency() },
            { VehicleTypes.Diplomat.ToString().ToLower(), () => new Diplomat() },
            { VehicleTypes.Foreign.ToString().ToLower(), () => new Foreign() },
            { VehicleTypes.Military.ToString().ToLower(), () => new Military() },
            { VehicleTypes.Bus.ToString().ToLower(), () => new Bus() }
        };
    }

    public Vehicle CreateVehicle(string name)
    {
        name = name.ToLower();
        return _map.ContainsKey(name)
            ? _map[name]()
            : new NotExistVehicle();
    }
}