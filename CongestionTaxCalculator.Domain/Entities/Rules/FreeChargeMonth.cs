using System;
using System.ComponentModel.DataAnnotations;

namespace CongestionTaxCalculator.Domain;

public class FreeChargeMonth
{
    [Key]
    public int Id { get; set; }
    public int Month { get; set; }
}
