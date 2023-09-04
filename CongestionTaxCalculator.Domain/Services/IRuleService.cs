using System;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.Services;

public interface IRuleService
{
    Task<DayPeriodTax> GetTaxItemByMovementTime(TimeOnly time);
    Task<bool> IsTollFreeVehicle(Vehicle vehicle);
    Task<bool> IsTollFreeMonth(DateTime date);
    Task<bool> IsTollFreeDayOfWeek(DateTime date);
    Task<bool> IsTollFreeDate(DateTime date);
    Task<bool> IsYearsValid(DateTime[] dates);
    bool IsMovementRangeValid(DateTime[] dates);
    Task<int> GetRuleMaxTax();
    Task<int> GetRuleMovementIntervalInMinute();
}