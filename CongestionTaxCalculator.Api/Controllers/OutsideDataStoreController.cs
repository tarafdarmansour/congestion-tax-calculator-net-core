using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculator.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OutsideDataStoreController : ControllerBase
    {
        private readonly IRuleService _ruleService;

        public OutsideDataStoreController(IRuleService ruleService)
        {
            _ruleService = ruleService;
        }

        [HttpPost]
        public DayPeriodTax GetTaxItemByMovementTime(DateTime time)
        {
            return _ruleService.GetTaxItemByMovementTime(TimeOnly.FromDateTime(time));
        }

        [HttpPost]
        public bool IsTollFreeVehicle(Vehicle vehicle)
        {
            return _ruleService.IsTollFreeVehicle(vehicle);
        }

        [HttpPost]
        public bool IsTollFreeMonth(DateTime date)
        {
            return _ruleService.IsTollFreeMonth(date);
        }

        [HttpPost]
        public bool IsTollFreeDayOfWeek(DateTime date)
        {
            return _ruleService.IsTollFreeDayOfWeek(date);
        }

        [HttpPost]
        public bool IsTollFreeDate(DateTime date)
        {
            return _ruleService.IsTollFreeDate(date);
        }

        [HttpPost]
        public bool IsYearsValid(DateTime[] dates)
        {
            return _ruleService.IsYearsValid(dates);
        }

        [HttpPost]
        public bool IsMovementRangeValid(DateTime[] dates)
        {
            return _ruleService.IsMovementRangeValid(dates);
        }

        [HttpPost]
        public int GetRuleMaxTax()
        {
            return _ruleService.GetRuleMaxTax();
        }

        [HttpPost]
        public int GetRuleMovementIntervalInMinute()
        {
            return _ruleService.GetRuleMovementIntervalInMinute();
        }
    }
}