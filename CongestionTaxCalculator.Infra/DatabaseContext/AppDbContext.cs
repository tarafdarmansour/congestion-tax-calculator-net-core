using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Infra.Comparer;
using CongestionTaxCalculator.Infra.Converter;
using Microsoft.EntityFrameworkCore;

namespace CongestionTaxCalculator.Infra.DatabaseContext;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=CongestionTaxCalculatorDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        base.OnConfiguring(optionsBuilder);
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        base.ConfigureConventions(builder);
        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter, DateOnlyComparer>()
            .HaveColumnType("date");

        builder.Properties<TimeOnly>()
            .HaveConversion<TimeOnlyConverter, TimeOnlyComparer>();
    }

    public DbSet<City> Cities { get; set; }
}