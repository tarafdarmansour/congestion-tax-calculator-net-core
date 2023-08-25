using System;
using System.ComponentModel.DataAnnotations;

namespace CongestionTaxCalculator.Domain;

public class DayPeriodTax
{
    [Key]
    public int Id { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int TaxFee { get; set; }
}
