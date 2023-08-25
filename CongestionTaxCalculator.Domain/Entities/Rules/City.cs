using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CongestionTaxCalculator.Domain;

public class City
{
    [Key] public int Id { get; set; }

    public string Name { get; set; }
    public ICollection<DayPeriodTax> DayPeriodTax { get; set; }
    public int DayMaxTax { get; set; }
    public ICollection<FreeChargeDayOfWeek> FreeChargeDayOfWeeks { get; set; }
    public ICollection<FreeChargeDate> FreeChargeDates { get; set; }
    public ICollection<FreeChargeMonth> FreeChargeMonths { get; set; }
    public ICollection<FreeChargeVehicle> FreeChargeVehicles { get; set; }

}