using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Services;
using CongestionTaxCalculator.Utilities.Helper;
using static System.Int32;

namespace CongestionTaxCalculator.Application;

public class GothenburgCongestionTaxCalculator
{
    private readonly ITaxService _taxService;
    private readonly int _maxTax;

    public GothenburgCongestionTaxCalculator(ITaxService taxService)
    {
        _taxService = taxService;
        _maxTax = _taxService.GetMaxTax();
    }

    public int GetTotalTax(Vehicle vehicle, DateTime[] movements)
    {
        if (IsTollFreeVehicle(vehicle)) return 0;
        ThrowIfDataRangeIsInvalid(movements);
        return GetTaxOfMovements(movements);
    }

    private void ThrowIfDataRangeIsInvalid(DateTime[] movements)
    {
        _taxService.ThrowIfDataRangeIsInvalid(movements);
    }

    private int GetTaxOfMovements(DateTime[] movements)
    {
        movements = movements.Order().ToArray();
        var intervalStart = movements[0];
        var taxOfDay = 0;
        foreach (var time in movements)
            taxOfDay += GetTimeMovementFee(time, ref intervalStart);

        if (taxOfDay > _maxTax) taxOfDay = _maxTax;
        return taxOfDay;
    }
    private int GetTimeMovementFee(DateTime time, ref DateTime intervalStart)
    {
        var nextFee = GetTollFee(time);
        var intervalStartFee = GetTollFee(intervalStart);

        var minutes = (time - intervalStart).TotalMinutes;
        if (minutes is <= 60 and > 0)
        {
            var intervalMax = Max(nextFee, intervalStartFee);
            var intervalMin = Min(nextFee, intervalStartFee);
            intervalStart = DateTimeHelper.GetMinDate(intervalStart, time);
            return intervalMax - intervalMin;
        }

        return nextFee;
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        return _taxService.IsTollFreeVehicle(vehicle);
    }

    private int GetTollFee(DateTime date)
    {
        return IsTollFreeDate(date) ? 0 : GetTollFeeByDateTime(date);
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