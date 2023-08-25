using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CongestionTaxCalculator.Infra.Comparer;

public class DateOnlyComparer : ValueComparer<DateOnly>
{
    public DateOnlyComparer() : base(
        (x, y) => x.DayNumber == y.DayNumber,
        dateOnly => dateOnly.GetHashCode())
    {
    }
}