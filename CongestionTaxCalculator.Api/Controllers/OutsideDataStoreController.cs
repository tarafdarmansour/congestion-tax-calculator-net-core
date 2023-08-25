using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Enums;
using CongestionTaxCalculator.Domain.Services;
using CongestionTaxCalculator.Infra.Factories;
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
        public DayPeriodTax GetTaxItemByMovementTime([FromBody] TimeOnly time)
        {
            return _ruleService.GetTaxItemByMovementTime(time);
        }
        [HttpPost]
        public bool IsTollFreeVehicle([FromBody]string vehicleType)
        {

            var vehicleFactory = new VehicleFactory();
            return _ruleService.IsTollFreeVehicle(vehicleFactory.CreateVehicle(vehicleType));
        }

        [HttpPost]
        public bool IsTollFreeMonth([FromBody] DateTime date)
        {
            return _ruleService.IsTollFreeMonth(date);
        }

        [HttpPost]
        public bool IsTollFreeDayOfWeek([FromBody] DateTime date)
        {
            return _ruleService.IsTollFreeDayOfWeek(date);
        }

        [HttpPost]
        public bool IsTollFreeDate([FromBody] DateTime date)
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

        [HttpGet]
        public int GetRuleMaxTax()
        {
            return _ruleService.GetRuleMaxTax();
        }

        [HttpGet]
        public int GetRuleMovementIntervalInMinute()
        {
            return _ruleService.GetRuleMovementIntervalInMinute();
        }


    }
}