using CongestionTaxCalculator.Domain;
using static System.Int32;

namespace CongestionTaxCalculator.Application;

public class GothenburgCongestionTaxCalculator
{
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

        var hour = date.Hour;
        var minute = date.Minute;

        if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        if (hour == 7 && minute >= 0 && minute <= 59) return 18;
        if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
        if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        if ((hour == 15 && minute >= 0) || (hour == 16 && minute <= 59)) return 18;
        if (hour == 17 && minute >= 0 && minute <= 59) return 13;
        if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        return 0;
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