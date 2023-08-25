using System;
using CongestionTaxCalculator.Shared.Exceptions;

namespace CongestionTaxCalculator.Domain.Exceptions;

public class InvalidMovementRangeException : BaseException
{
    public InvalidMovementRangeException() : base($"Movement dates is not same.")
    {
    }
}