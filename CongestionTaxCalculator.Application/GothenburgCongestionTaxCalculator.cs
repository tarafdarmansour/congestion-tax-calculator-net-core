using CongestionTaxCalculator.Domain;
using System.Diagnostics;
using static System.Int32;

namespace CongestionTaxCalculator.Application;

public partial class GothenburgCongestionTaxCalculator
{
    private List<PeriodTax> taxList;
    public GothenburgCongestionTaxCalculator()
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
    /**
             * Calculate the total toll fee for one day
             *
             * @param vehicle - the vehicle
             * @param dates   - date and time of all passes on one day
             * @return - the total congestion tax for that day
             */
    public int GetTotalTax(Vehicle vehicle, DateTime[] movements)
    {
        if (IsTollFreeVehicle(vehicle)) return 0;

        var groupedDates = GetMovementGroupedByDate(movements);
        return GetTotalTaxOfMovements(groupedDates);
    }

    private int GetTotalTaxOfMovements(IEnumerable<IGrouping<DateTime, DateTime>> groupedDates)
    {
        var totalTax = 0;
        foreach (var movementsInDay in groupedDates)
        {
            var taxOfDay = GetTaxOfDay(movementsInDay);
            totalTax += taxOfDay;
        }

        return totalTax;
    }

    private static IEnumerable<IGrouping<DateTime, DateTime>> GetMovementGroupedByDate(DateTime[] movements)
    {
        movements = movements.Distinct().ToList().Order().ToArray();
        var groupedDates = movements.GroupBy(d => d.Date);
        return groupedDates;
    }

    private int GetTaxOfDay(IGrouping<DateTime, DateTime> groupedDate)
    {
        var dayMovements = groupedDate.ToList();
        var intervalStart = dayMovements[0];
        var taxOfDay = 0;
        foreach (var time in dayMovements) 
            intervalStart = GetTimeMovementFee(time, intervalStart, ref taxOfDay);

        if (taxOfDay > 60) taxOfDay = 60;
        return taxOfDay;
    }

    private DateTime GetTimeMovementFee(DateTime time, DateTime intervalStart, ref int taxOfDay)
    {
        var nextFee = GetTollFee(time);
        var intervalStartFee = GetTollFee(intervalStart);

        var minutes = (time - intervalStart).TotalMinutes;
        if (minutes is <= 60 and > 0)
        {
            var intervalMax = Max(nextFee, intervalStartFee);
            var intervalMin = Min(nextFee, intervalStartFee);
            taxOfDay += intervalMax - intervalMin;

            intervalStart = GetMinDate(intervalStart, time);
        }
        else
        {
            taxOfDay += nextFee;
        }

        return intervalStart;
    }

    private DateTime GetMinDate(DateTime value1, DateTime value2)
    {
        return value1 < value2 ? value1 : value2;
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;
        var vehicleType = vehicle.GetVehicleType();
        return vehicleType.Equals(TollFreeVehicles.Motorcycle.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Military.ToString());
    }

    public int GetTollFee(DateTime date)
    {
        if (IsTollFreeDate(date)) return 0;

        return GetTollFeeByDateTime(date);
    }

    private int GetTollFeeByDateTime(DateTime date)
    {
        var timeOfDate = TimeOnly.FromDateTime(date);
        var taxItem = taxList.FirstOrDefault(t => t.StartTime <= timeOfDate && t.EndTime >= timeOfDate);
        if (taxItem == null) return 0;
        return taxItem.TaxFee;
    }

    private bool IsTollFreeDate(DateTime date)
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

    private enum TollFreeVehicles
    {
        Motorcycle = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }
}