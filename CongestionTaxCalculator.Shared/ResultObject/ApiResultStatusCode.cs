using System.ComponentModel.DataAnnotations;

namespace CongestionTaxCalculator.Shared.ResultObject;

public enum ApiResultStatusCode
{
    [Display(Name = "عملیات با موفقیت انجام شد")]
    Success = 0,

    [Display(Name = "خطایی رخ داده")]
    ServerError = 1,
}