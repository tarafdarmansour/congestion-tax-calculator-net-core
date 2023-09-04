
using CongestionTaxCalculator.Application;
using CongestionTaxCalculator.Domain.Repositories;
using CongestionTaxCalculator.Domain.Services;
using CongestionTaxCalculator.Infra.DatabaseContext;
using CongestionTaxCalculator.Infra.Repositories;
using CongestionTaxCalculator.Infra;
using CongestionTaxCalculator.EndPointApi.Middleware;

namespace CongestionTaxCalculator.EndPointApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<AppDbContext>();
            builder.Services.AddScoped<ITaxService, TaxService>();
            builder.Services.AddScoped<IRuleService, RuleService>();
            builder.Services.AddScoped<ICongestionTaxCalculator, GothenburgCongestionTaxCalculator>();
            builder.Services.AddScoped<IRuleRepository, GothenburgRuleRepository>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}