using System;
using System.ComponentModel.DataAnnotations;

namespace CongestionTaxCalculator.Domain;

public class AcceptableYear
{
    [Key]
    public int Id { get; set; }
    public int Year { get; set; }
}
