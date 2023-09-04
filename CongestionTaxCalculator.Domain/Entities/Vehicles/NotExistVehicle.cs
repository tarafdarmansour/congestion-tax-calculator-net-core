using CongestionTaxCalculator.Domain.Exceptions;
using System;

namespace CongestionTaxCalculator.Domain
{
    public class NotExistVehicle : Vehicle
    {
        public string GetVehicleType()
        {
            throw new VehicleDoesNotExistException();
        }
    }
}