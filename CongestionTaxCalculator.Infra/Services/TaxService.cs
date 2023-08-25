using CongestionTaxCalculator.Domain.Enums;
using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Services;
using System.Diagnostics;
using CongestionTaxCalculator.Application;
using CongestionTaxCalculator.Utilities;
using CongestionTaxCalculator.Domain.Repositories;

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
        var year = date.Year;
        var month = date.Month;
        var day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
            if ((month == 1 && day == 1) ||
                (month == 3 && (day == 28 || day == 29)) ||
                (month == 4 && (day == 1 || day == 30)) ||
                (month == 5 && (day == 1 || day == 8 || day == 9)) ||
                (month == 6 && (day == 5 || day == 6 || day == 21)) ||
                month == 7 ||
                (month == 11 && day == 1) ||
                (month == 12 && (day == 24 || day == 25 || day == 26 || day == 31)))
                return true;
        return false;
    }
    public int GetTollFeeByDateTime(DateTime date)
    {
        var timeOfDate = TimeOnly.FromDateTime(date);
        var taxItem = _ruleService.GetTaxItemByMovementTime(timeOfDate);
        if (taxItem == null) return 0;
        return taxItem.TaxFee;
    }
    public bool IsTollFreeVehicle(Vehicle vehicle)
    {
        return TollFreeVehicles.Diplomat.ToList().Any(v => v == vehicle.GetVehicleType());
    }
}