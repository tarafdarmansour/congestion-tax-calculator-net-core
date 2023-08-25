﻿using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Enums;
using CongestionTaxCalculator.Domain.Repositories;
using CongestionTaxCalculator.Domain.Services;
using CongestionTaxCalculator.Infra.DatabaseContext;
using CongestionTaxCalculator.Utilities;

namespace CongestionTaxCalculator.Infra;

public class RuleService : IRuleService
{
    private readonly IRuleRepository _ruleRepository;

    public RuleService(IRuleRepository ruleRepository)
    {
        _ruleRepository = ruleRepository;
    }
    public DayPeriodTax? GetTaxItemByMovementTime(TimeOnly time)
    {
        return _ruleRepository
            .GetCityDayPeriodTaxes()
            .FirstOrDefault(t => t.StartTime <= time && t.EndTime >= time);
    }

    public bool IsTollFreeVehicle(Vehicle vehicle)
    {
        return _ruleRepository.GetTollFreeVehicles().Any(v => v.Name == vehicle.GetVehicleType());
    }
}