using System;
using CongestionTaxCalculator.Shared.Exceptions;

namespace CongestionTaxCalculator.Domain.Exceptions;

public class InvalidYearException : BaseException
{
    public InvalidYearException() : base($"Year of DateRange is invalid.")
    {
    }
}