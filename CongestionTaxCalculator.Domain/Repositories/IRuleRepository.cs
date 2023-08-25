using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.Repositories
{
    public interface IRuleRepository
    {
        abstract string GetRuleName ();
        List<DayPeriodTax> GetCityDayPeriodTaxes();
        List<FreeChargeVehicle> GetTollFreeVehicles();
        List<FreeChargeMonth> GetTollFreeMonths();
        List<FreeChargeDayOfWeek> GetTollFreeDayOfWeeks();
        List<FreeChargeDate> GetTollFreeDates();
        List<AcceptableYear> GetAcceptableYears();
        City GetCityRule();
    }
}
