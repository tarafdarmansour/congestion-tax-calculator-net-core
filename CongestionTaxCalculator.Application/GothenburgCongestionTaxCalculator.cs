using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Enums;
using CongestionTaxCalculator.Domain.Services;
using System.Diagnostics;
using static System.Int32;

namespace CongestionTaxCalculator.Application;

public partial class GothenburgCongestionTaxCalculator
{
    private readonly ITaxService _taxService;
    public GothenburgCongestionTaxCalculator(ITaxService taxService)
    {
        _taxService = taxService;
        
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

    private static DateTime GetMinDate(DateTime value1, DateTime value2)
    {
        return value1 < value2 ? value1 : value2;
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        return _taxService.IsTollFreeVehicle(vehicle);
    }

    private int GetTollFee(DateTime date)
    {
        if (IsTollFreeDate(date)) return 0;

        return GetTollFeeByDateTime(date);
    }

    private int GetTollFeeByDateTime(DateTime date)
    {
        return _taxService.GetTollFeeByDateTime(date);
    }

    private bool IsTollFreeDate(DateTime date)
    {
        return _taxService.IsTollFreeDate(date);
    }
}