﻿using CongestionTaxCalculator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.UnitTest.Factories
{
    public class VehicleFactory
    {
        private readonly Dictionary<string, Func<Vehicle>> _map;

        public VehicleFactory()
        {
            _map = new Dictionary<string, Func<Vehicle>>();
            _map.Add(VehicleTypes.Car.ToString(), () => new Car());
            _map.Add(VehicleTypes.Motorcycle.ToString(), () => new Motorbike());
        }

        public Vehicle CreateVehicle(string name)
        {
            return _map.ContainsKey(name)
                ? _map[name]()
                : new NotExistVehicle();
        }

    }
}