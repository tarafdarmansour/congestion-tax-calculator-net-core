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
        return _ruleRepository
            .GetTollFreeDates()
            .Any(v => v.FreeOfChargeDate == DateOnly.FromDateTime(date) || 
                      v.FreeOfChargeDate.AddDays(-1) == DateOnly.FromDateTime(date));
    }
    
    public bool IsYearsValid(DateTime[] dates)
    {
        var years = dates.GroupBy(d => d.Year).Select(g => g.Key).ToList();
        if (years.Count is > 1 or 0) return false;
        return _ruleRepository.GetAcceptableYears().Any(v => v.Year == years.First());
    }
    public bool IsMovementRangeValid(DateTime[] dates)
    {
       return dates.GroupBy(d => d.Date).Distinct().Count() == 1;
    }

    public int GetRuleMaxTax()
    {
        return _ruleRepository.GetCityRule()?.DayMaxTax ?? 0;
    }
}