namespace CongestionTaxCalculator.Application;

public partial class GothenburgCongestionTaxCalculator
{
    public class PeriodTax
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int TaxFee { get; set; }
    }
}