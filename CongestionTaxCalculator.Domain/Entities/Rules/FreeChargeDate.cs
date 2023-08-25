using System;
using System.ComponentModel.DataAnnotations;

namespace CongestionTaxCalculator.Domain;

public class FreeChargeDate
{
    [Key]
    public int Id { get; set; }
    public DateOnly FreeOfChargeDate { get; set; }
}
