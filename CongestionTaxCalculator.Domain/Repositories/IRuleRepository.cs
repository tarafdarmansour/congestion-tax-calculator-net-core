using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.Repositories
{
    public interface IRuleRepository
    {
        Task<List<DayPeriodTax>> GetCityDayPeriodTaxes();
        Task<List<FreeChargeVehicle>> GetTollFreeVehicles();
        Task<List<FreeChargeMonth>> GetTollFreeMonths();
        Task<List<FreeChargeDayOfWeek>> GetTollFreeDayOfWeeks();
        Task<List<FreeChargeDate>> GetTollFreeDates();
        Task<List<AcceptableYear>> GetAcceptableYears();
        Task<City> GetCityRule();
    }
}
