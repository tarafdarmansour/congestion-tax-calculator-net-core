using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Utilities.Helper
{
    public static class DateTimeHelper
    {
        public static DateTime GetMinDate(DateTime value1, DateTime value2)
        {
            return value1 < value2 ? value1 : value2;
        }
    }
}
