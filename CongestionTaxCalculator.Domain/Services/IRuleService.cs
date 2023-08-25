using System;

namespace CongestionTaxCalculator.Domain.Services;

public interface IRuleService
{
    DayPeriodTax GetTaxItemByMovementTime(TimeOnly time);
    bool IsTollFreeVehicle(Vehicle vehicle);
    bool IsTollFreeMonth(DateTime date);
    bool IsTollFreeDayOfWeek(DateTime date);
    bool IsTollFreeDate(DateTime date);
}