using System;
using System.ComponentModel.DataAnnotations;

namespace CongestionTaxCalculator.Domain;

public class FreeChargeVehicle
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}
