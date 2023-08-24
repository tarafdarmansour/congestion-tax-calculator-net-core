using CongestionTaxCalculator.Domain.Enums;
using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Services;
using System.Diagnostics;
using CongestionTaxCalculator.Application;
using CongestionTaxCalculator.Utilities;

namespace CongestionTaxCalculator.Infra;
public class TaxService : ITaxService
{
    private List<PeriodTax> taxList;
    public TaxService()
    {
        taxList = new List<PeriodTax>()
        {
            new() { StartTime = new TimeOnly(0, 0, 0, 0), EndTime = new TimeOnly(5, 59, 59, 999), TaxFee = 0 },
            new() { StartTime = new TimeOnly(6, 0, 0, 0), EndTime = new TimeOnly(6, 29, 59, 999), TaxFee = 8 },
            new() { StartTime = new TimeOnly(6, 30, 0, 0), EndTime = new TimeOnly(6, 59, 59, 999), TaxFee = 13 },
            new() { StartTime = new TimeOnly(7, 0, 0, 0), EndTime = new TimeOnly(7, 59, 59, 999), TaxFee = 18 },
            new() { StartTime = new TimeOnly(8, 0, 0, 0), EndTime = new TimeOnly(8, 29, 59, 999), TaxFee = 13 },
            new() { StartTime = new TimeOnly(8, 30, 0, 0), EndTime = new TimeOnly(14, 59, 59, 999), TaxFee = 8 },
            new() { StartTime = new TimeOnly(15, 0, 0, 0), EndTime = new TimeOnly(15, 29, 59, 999), TaxFee = 13 },
            new() { StartTime = new TimeOnly(15, 30, 0, 0), EndTime = new TimeOnly(16, 59, 59, 999), TaxFee = 18 },
            new() { StartTime = new TimeOnly(17, 0, 0, 0), EndTime = new TimeOnly(17, 59, 59, 999), TaxFee = 13 },
            new() { StartTime = new TimeOnly(18, 0, 0, 0), EndTime = new TimeOnly(18, 29, 59, 999), TaxFee = 8 },
            new() { StartTime = new TimeOnly(18, 30, 0, 0), EndTime = new TimeOnly(23, 59, 59, 999), TaxFee = 0 },
        };
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
        var taxItem = taxList.FirstOrDefault(t => t.StartTime <= timeOfDate && t.EndTime >= timeOfDate);
        if (taxItem == null) return 0;
        return taxItem.TaxFee;
    }
    public bool IsTollFreeVehicle(Vehicle vehicle)
    {
        return TollFreeVehicles.Diplomat.ToList().Any(v => v == vehicle.GetVehicleType());
    }
}