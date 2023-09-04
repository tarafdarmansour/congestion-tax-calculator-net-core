using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.EndPointApi.Models
{
    public record CalculateCarTaxRequest
    {
        public CalculateCarTaxRequest(string vehicle, DateTime[] movements)
        {
            Movements = movements;
            Vehicle = vehicle;
        }
        public string Vehicle { get; }
        public DateTime[] Movements { get; }
    }
}
