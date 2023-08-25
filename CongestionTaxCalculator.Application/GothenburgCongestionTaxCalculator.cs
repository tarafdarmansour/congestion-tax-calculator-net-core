using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Services;
using CongestionTaxCalculator.Utilities.Helper;
using static System.Int32;

namespace CongestionTaxCalculator.Application;

public class GothenburgCongestionTaxCalculator
{
    private readonly int _maxTax;
    private readonly ITaxService _taxService;

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
    private int GetTaxOfMovements(DateTime[] movements)
    {
        var points = GetGroupedMovementsPoints(movements);
        var taxOfDay = CalculateTaxOfDay(points);
        return taxOfDay > _maxTax ? _maxTax : taxOfDay;
    }
    private static int CalculateTaxOfDay(List<MovementsPoint> points)
    {
        var groupCount = points.Select(p => p.GroupId).Max();
        var taxOfDay = 0;
        for (var i = 1; i <= groupCount; i++)
        {
            taxOfDay += points.Where(p => p.GroupId == i).Select(p => p.Tax).Max();
        }
        return taxOfDay;
    }
    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        return _taxService.IsTollFreeVehicle(vehicle);
    }
    private void ThrowIfDataRangeIsInvalid(DateTime[] movements)
    {
        _taxService.ThrowIfDataRangeIsInvalid(movements);
    }
    private List<MovementsPoint> GetGroupedMovementsPoints(DateTime[] movements)
    {
        var points = InitiateMovementsPoints(movements);
        GroupMovementPoints(points);
        return points;
    }
    private static void GroupMovementPoints(List<MovementsPoint> points)
    {
        int groupIndex = 1;
        foreach (var point in points)
        {
            var group = points.Where(p => Math.Abs((p.Time - point.Time).TotalMinutes) <= 60 && p.GroupId == 0).ToList();
            if (group.Count > 0)
            {
                foreach (var movementsPoint in group)
                {
                    movementsPoint.GroupId = groupIndex;
                }

                groupIndex++;
            }
        }
    }
    private List<MovementsPoint> InitiateMovementsPoints(DateTime[] movements)
    {
        return movements
            .Select(m => new MovementsPoint { Time = m, Tax = GetTollFee(m) })
            .ToList();
    }
    private int GetTollFee(DateTime date)
    {
        return _taxService.IsTollFreeDate(date) ? 0 : _taxService.GetTollFeeByDateTime(date);
    }
}