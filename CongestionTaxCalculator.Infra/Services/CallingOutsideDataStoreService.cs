using System.Net.Http.Headers;
using System.Text;
using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Services;
using Newtonsoft.Json;

namespace CongestionTaxCalculator.Infra;

public class CallingOutsideDataStoreService : IRuleService
{
    private readonly HttpClient _httpClient;

    public CallingOutsideDataStoreService()
    {
        _httpClient = new HttpClient();
    }

    public Task<DayPeriodTax> GetTaxItemByMovementTime(TimeOnly time)
    {
        return CallPostService<DayPeriodTax>("GetTaxItemByMovementTime", time);
    }

    public Task<bool> IsTollFreeVehicle(Vehicle vehicle)
    {
        return CallPostService<bool>("IsTollFreeVehicle", vehicle.GetVehicleType());
    }

    public Task<bool> IsTollFreeMonth(DateTime date)
    {
        return CallPostService<bool>("IsTollFreeMonth", date);
    }

    public Task<bool> IsTollFreeDayOfWeek(DateTime date)
    {
        return CallPostService<bool>("IsTollFreeDayOfWeek", date);
    }

    public Task<bool> IsTollFreeDate(DateTime date)
    {
        return CallPostService<bool>("IsTollFreeDate", date);
    }

    public Task<bool> IsYearsValid(DateTime[] dates)
    {
        return CallPostService<bool>("IsYearsValid", dates);
    }

    public bool IsMovementRangeValid(DateTime[] dates)
    {
        return CallPostService<bool>("IsMovementRangeValid", dates).GetAwaiter().GetResult();
    }

    public Task<int> GetRuleMaxTax()
    {
        return CallGetService<int>("GetRuleMaxTax");
    }

    public Task<int> GetRuleMovementIntervalInMinute()
    {
        return CallGetService<int>("GetRuleMovementIntervalInMinute");
    }

    private async Task<TResult> CallPostService<TResult>(string method, object data)
    {
        var myContent = JsonConvert.SerializeObject(data);
        var buffer = Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var resp = await _httpClient
            .PostAsync($"https://localhost:7119/OutsideDataStore/{method}", byteContent);
            
        var content = JsonConvert.DeserializeObject<TResult>(await resp.Content.ReadAsStringAsync());
        return content;
    }

    private async Task<TResult> CallGetService<TResult>(string method)
    {
        var resp = await _httpClient
            .GetStringAsync($"https://localhost:7119/OutsideDataStore/{method}");
        var content = JsonConvert.DeserializeObject<TResult>(resp);
        return content;
    }
}