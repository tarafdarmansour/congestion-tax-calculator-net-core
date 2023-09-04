namespace CongestionTaxCalculator.Application;

public interface ICongestionTaxCalculator
{
    Task<int> GetTotalTax(CalculateCarTaxRequestDto requestDto);
}