using System.Security.Cryptography.X509Certificates;
using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Repositories;
using CongestionTaxCalculator.Infra.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CongestionTaxCalculator.Infra.Repositories;

public class GothenburgRuleRepository : IRuleRepository
{
    private readonly AppDbContext _appDbContext;

    public GothenburgRuleRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public List<DayPeriodTax> GetCityDayPeriodTaxes()
    {
        return _appDbContext.Cities
            .Include(c => c.DayPeriodTax)
            .First(c => c.Name == GetRuleName())
            .DayPeriodTax
            .ToList();
    }
    public string GetRuleName()
    {
        return "Gothenburg";
    }

    public List<FreeChargeVehicle> GetTollFreeVehicles()
    {
        return _appDbContext.Cities
            .Include(c => c.FreeChargeVehicles)
            .First(c => c.Name == GetRuleName())
            .FreeChargeVehicles
            .ToList();
    }

    public List<FreeChargeMonth> GetTollFreeMonths()
    {
        return _appDbContext.Cities
            .Include(c => c.FreeChargeMonths)
            .First(c => c.Name == GetRuleName())
            .FreeChargeMonths
            .ToList();
    }

    public List<FreeChargeDayOfWeek> GetTollFreeDayOfWeeks()
    {
        return _appDbContext.Cities
            .Include(c => c.FreeChargeDayOfWeeks)
            .First(c => c.Name == GetRuleName())
            .FreeChargeDayOfWeeks
            .ToList();
    }

    public List<FreeChargeDate> GetTollFreeDates()
    {
        return _appDbContext.Cities
            .Include(c => c.FreeChargeDates)
            .First(c => c.Name == GetRuleName())
            .FreeChargeDates
            .ToList();
    }

    public List<AcceptableYear> GetAcceptableYears()
    {
        return _appDbContext.Cities
            .Include(c => c.AcceptableYears)
            .First(c => c.Name == GetRuleName())
            .AcceptableYears
            .ToList();
    }

    public City? GetCityRule()
    {
        return _appDbContext.Cities.FirstOrDefault(c => c.Name == GetRuleName());
    }
}