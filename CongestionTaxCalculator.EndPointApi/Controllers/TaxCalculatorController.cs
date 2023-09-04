using CongestionTaxCalculator.Application;
using CongestionTaxCalculator.EndPointApi.Models;
using CongestionTaxCalculator.Shared.ResultObject;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculator.EndPointApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TaxCalculatorController : ControllerBase
{
    private readonly ICongestionTaxCalculator _taxCalculator;

    public TaxCalculatorController(ICongestionTaxCalculator taxCalculator)
    {
        _taxCalculator = taxCalculator;
    }

    [HttpPost]
    public async Task<ApiResult<CalculateCarTaxResponse>> CalculateTax(CalculateCarTaxRequest requestDto)
    {
        var result =
            await _taxCalculator.GetTotalTax(new CalculateCarTaxRequestDto(requestDto.Vehicle, requestDto.Movements));

        return new ApiResult<CalculateCarTaxResponse>(true, ApiResultStatusCode.Success,
            new CalculateCarTaxResponse(result));
    }
}