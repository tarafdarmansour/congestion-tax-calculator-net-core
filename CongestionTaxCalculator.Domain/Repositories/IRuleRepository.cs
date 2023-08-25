using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.Repositories
{
    public interface IRuleRepository
    {
        abstract string GetCityName ();
        List<DayPeriodTax> GetCityDayPeriodTaxes();
    }
}
