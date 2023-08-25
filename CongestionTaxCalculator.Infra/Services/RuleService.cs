using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Enums;
using CongestionTaxCalculator.Domain.Repositories;
using CongestionTaxCalculator.Domain.Services;
using CongestionTaxCalculator.Infra.DatabaseContext;
using CongestionTaxCalculator.Utilities;

namespace CongestionTaxCalculator.Infra;

public class RuleService : IRuleService
{
    private readonly IRuleRepository _ruleRepository;

    public RuleService(IRuleRepository ruleRepository)
    {
        _ruleRepository = ruleRepository;
    }
    public DayPeriodTax? GetTaxItemByMovementTime(TimeOnly time)
    {
        return _ruleRepository
            .GetCityDayPeriodTaxes()
            .FirstOrDefault(t => t.StartTime <= time && t.EndTime >= time);
    }

    public bool IsTollFreeVehicle(Vehicle vehicle)
    {
        return _ruleRepository.GetTollFreeVehicles().Any(v => v.Name == vehicle.GetVehicleType());
    }
    public bool IsTollFreeMonth(DateTime date)
    {
        return _ruleRepository.GetTollFreeMonths().Any(v => v.Month == date.Month);
    }
    public bool IsTollFreeDayOfWeek(DateTime date)
    {
        return _ruleRepository.GetTollFreeDayOfWeeks().Any(v => v.DayOfWeek == date.DayOfWeek);
    }
    public bool IsTollFreeDate(DateTime date)
    {
        return _ruleRepository.GetTollFreeDates().Any(v => v.FreeOfChargeDate == DateOnly.FromDateTime(date));
    }
}