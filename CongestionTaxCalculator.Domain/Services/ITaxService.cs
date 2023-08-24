using System;

namespace CongestionTaxCalculator.Domain.Services;

public interface ITaxService
{
    bool IsTollFreeDate(DateTime date);
    int GetTollFeeByDateTime(DateTime date);
    bool IsTollFreeVehicle(Vehicle vehicle);
}