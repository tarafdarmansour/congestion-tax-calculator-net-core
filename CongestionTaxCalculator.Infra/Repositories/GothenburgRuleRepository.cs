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
            .First(c => c.Name == GetCityName())
            .DayPeriodTax
            .ToList();
    }
    public string GetCityName()
    {
        return "Gothenburg";
    }

    public List<FreeChargeVehicle> GetTollFreeVehicles()
    {
        return _appDbContext.Cities
            .Include(c => c.FreeChargeVehicles)
            .First(c => c.Name == GetCityName())
            .FreeChargeVehicles
            .ToList();
    }
}