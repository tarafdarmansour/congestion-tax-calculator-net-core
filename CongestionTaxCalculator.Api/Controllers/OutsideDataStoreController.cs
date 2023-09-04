using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Enums;
using CongestionTaxCalculator.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

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
        public async Task<DayPeriodTax> GetTaxItemByMovementTime([FromBody] TimeOnly time)
        {
            return await _ruleService.GetTaxItemByMovementTime(time);
        }
        [HttpPost]
        public async Task<bool> IsTollFreeVehicle([FromBody]string vehicleType)
        {

            var vehicleFactory = new VehicleFactory();
            return await _ruleService.IsTollFreeVehicle(vehicleFactory.CreateVehicle(vehicleType));
        }

        [HttpPost]
        public async Task<bool> IsTollFreeMonth([FromBody] DateTime date)
        {
            return await _ruleService.IsTollFreeMonth(date);
        }

        [HttpPost]
        public async Task<bool> IsTollFreeDayOfWeek([FromBody] DateTime date)
        {
            return await _ruleService.IsTollFreeDayOfWeek(date);
        }

        [HttpPost]
        public async Task<bool> IsTollFreeDate([FromBody] DateTime date)
        {
            return await _ruleService.IsTollFreeDate(date);
        }

        [HttpPost]
        public async Task<bool> IsYearsValid(DateTime[] dates)
        {
            return await _ruleService.IsYearsValid(dates);
        }

        [HttpPost]
        public bool IsMovementRangeValid(DateTime[] dates)
        {
            return _ruleService.IsMovementRangeValid(dates);
        }

        [HttpGet]
        public async Task<int> GetRuleMaxTax()
        {
            return await _ruleService.GetRuleMaxTax();
        }

        [HttpGet]
        public async Task<int> GetRuleMovementIntervalInMinute()
        {
            return await _ruleService.GetRuleMovementIntervalInMinute();
        }


    }
}