using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Exceptions;
using CongestionTaxCalculator.Domain.Services;

namespace CongestionTaxCalculator.Infra;

public class TaxService : ITaxService
{
    private readonly IRuleService _ruleService;

    public TaxService(IRuleService ruleService)
    {
        _ruleService = ruleService;
    }

    public bool IsTollFreeDate(DateTime date)
    {
        if (_ruleService.IsTollFreeMonth(date).GetAwaiter().GetResult()) return true;
        if (_ruleService.IsTollFreeDayOfWeek(date).GetAwaiter().GetResult()) return true;
        return _ruleService.IsTollFreeDate(date).GetAwaiter().GetResult();
    }

    public int GetTollFeeByDateTime(DateTime date)
    {
        return _ruleService.GetTaxItemByMovementTime(TimeOnly.FromDateTime(date)).GetAwaiter().GetResult()?.TaxFee ?? 0;
    }

    public bool IsTollFreeVehicle(Vehicle vehicle)
    {
        return _ruleService.IsTollFreeVehicle(vehicle).GetAwaiter().GetResult();
    }

    public void ThrowIfDataRangeIsInvalid(DateTime[] dataRange)
    {
        if (!_ruleService.IsMovementRangeValid(dataRange))
            throw new InvalidMovementRangeException();
        if(!_ruleService.IsYearsValid(dataRange).GetAwaiter().GetResult())
            throw new InvalidYearException();
    }

    public int GetMaxTax()
    {
        return _ruleService.GetRuleMaxTax().GetAwaiter().GetResult();
    }

    public int GetRuleMovementIntervalInMinute()
    {
        return _ruleService.GetRuleMovementIntervalInMinute().GetAwaiter().GetResult();
    }
}