using System;
using CongestionTaxCalculator.Shared.Exceptions;

namespace CongestionTaxCalculator.Domain.Exceptions;

public class VehicleDoesNotExistException : BaseException
{
    public VehicleDoesNotExistException() : base($"Vehicle name is not valid.")
    {
    }
}