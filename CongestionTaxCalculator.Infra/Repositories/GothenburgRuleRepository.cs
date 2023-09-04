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

    public async Task<List<DayPeriodTax>> GetCityDayPeriodTaxes()
    {
        return (await _appDbContext.Cities
            .Include(c => c.DayPeriodTax)
            .FirstAsync(c => c.Name == GetRuleName()))
            .DayPeriodTax
            .ToList();
    }
    private string GetRuleName()
    {
        return "Gothenburg";
    }

    public async Task<List<FreeChargeVehicle>> GetTollFreeVehicles()
    {
        return  (await _appDbContext.Cities
            .Include(c => c.FreeChargeVehicles)
            .FirstAsync(c => c.Name == GetRuleName()))
            .FreeChargeVehicles
            .ToList();
    }

    public async Task<List<FreeChargeMonth>> GetTollFreeMonths()
    {
        return (await _appDbContext.Cities
            .Include(c => c.FreeChargeMonths)
            .FirstAsync(c => c.Name == GetRuleName()))
            .FreeChargeMonths
            .ToList();
    }

    public async Task<List<FreeChargeDayOfWeek>> GetTollFreeDayOfWeeks()
    {
        return (await _appDbContext.Cities
            .Include(c => c.FreeChargeDayOfWeeks)
            .FirstAsync(c => c.Name == GetRuleName()))
            .FreeChargeDayOfWeeks
            .ToList();
    }

    public async Task<List<FreeChargeDate>> GetTollFreeDates()
    {
        return (await _appDbContext.Cities
            .Include(c => c.FreeChargeDates)
            .FirstAsync(c => c.Name == GetRuleName()))
            .FreeChargeDates
            .ToList();
    }

    public async Task<List<AcceptableYear>> GetAcceptableYears()
    {
        return (await _appDbContext.Cities
            .Include(c => c.AcceptableYears)
            .FirstAsync(c => c.Name == GetRuleName()))
            .AcceptableYears
            .ToList();
    }

    public async Task<City?> GetCityRule()
    {
        return await _appDbContext.Cities.FirstOrDefaultAsync(c => c.Name == GetRuleName());
    }
}