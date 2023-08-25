using System;

namespace CongestionTaxCalculator.Domain.Services;

public interface IRuleService
{
    DayPeriodTax GetTaxItemByMovementTime(TimeOnly time);
}