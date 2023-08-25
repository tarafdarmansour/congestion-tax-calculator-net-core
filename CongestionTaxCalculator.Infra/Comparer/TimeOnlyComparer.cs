using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CongestionTaxCalculator.Infra.Comparer;

public class TimeOnlyComparer : ValueComparer<TimeOnly>
{
    public TimeOnlyComparer() : base(
        (x, y) => x.Ticks == y.Ticks,
        timeOnly => timeOnly.GetHashCode())
    {
    }
}