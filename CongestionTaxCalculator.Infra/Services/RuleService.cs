using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Repositories;
using CongestionTaxCalculator.Domain.Services;

namespace CongestionTaxCalculator.Infra;

public class RuleService : IRuleService
{
    private readonly IRuleRepository _ruleRepository;

    public RuleService(IRuleRepository ruleRepository)
    {
        _ruleRepository = ruleRepository;
    }

    public async Task<DayPeriodTax?> GetTaxItemByMovementTime(TimeOnly time)
    {
        return (await _ruleRepository
            .GetCityDayPeriodTaxes())
            .FirstOrDefault(t => t.StartTime <= time && t.EndTime >= time);
    }

    public async Task<bool> IsTollFreeVehicle(Vehicle vehicle)
    {
        return (await _ruleRepository.GetTollFreeVehicles()).Any(v => v.Name == vehicle.GetVehicleType());
    }

    public async Task<bool> IsTollFreeMonth(DateTime date)
    {
        return (await _ruleRepository.GetTollFreeMonths()).Any(v => v.Month == date.Month);
    }

    public async Task<bool> IsTollFreeDayOfWeek(DateTime date)
    {
        return (await _ruleRepository.GetTollFreeDayOfWeeks()).Any(v => v.DayOfWeek == date.DayOfWeek);
    }

    public async Task<bool> IsTollFreeDate(DateTime date)
    {
        return (await _ruleRepository
            .GetTollFreeDates())
            .Any(v => v.FreeOfChargeDate == DateOnly.FromDateTime(date) ||
                      v.FreeOfChargeDate.AddDays(-1) == DateOnly.FromDateTime(date));
    }

    public async Task<bool> IsYearsValid(DateTime[] dates)
    {
        var years = dates.GroupBy(d => d.Year).Select(g => g.Key).ToList();
        if (years.Count is > 1 or 0) return false;
        return (await _ruleRepository.GetAcceptableYears()).Any(v => v.Year == years.First());
    }

    public bool IsMovementRangeValid(DateTime[] dates)
    {
        var count = dates.GroupBy(d => d.Date).Distinct().Count();
        return count == 1;
    }

    public async Task<int> GetRuleMaxTax()
    {
        return (await _ruleRepository.GetCityRule())?.DayMaxTax ?? 0;
    }

    public async Task<int> GetRuleMovementIntervalInMinute()
    {
        return (await _ruleRepository.GetCityRule())?.MovementIntervalInMinute ?? 0;
    }
}