using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.EndPointApi.Models
{
    public record CalculateCarTaxResponse
    {
        public CalculateCarTaxResponse(int taxAmount)
        {
            this.TaxAmount = taxAmount;
        }
        public int TaxAmount { get; }
    }
}
