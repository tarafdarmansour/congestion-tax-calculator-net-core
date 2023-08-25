using System;
using System.ComponentModel.DataAnnotations;

namespace CongestionTaxCalculator.Domain;

public class FreeChargeDayOfWeek
{
    [Key]
    public int Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
}
