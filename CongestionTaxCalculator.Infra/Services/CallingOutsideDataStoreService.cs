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

    public DayPeriodTax GetTaxItemByMovementTime(TimeOnly time)
    {
        return CallPostService<DayPeriodTax>("GetTaxItemByMovementTime", time);
    }

    public bool IsTollFreeVehicle(Vehicle vehicle)
    {
        return CallPostService<bool>("IsTollFreeVehicle", vehicle.GetVehicleType());
    }

    public bool IsTollFreeMonth(DateTime date)
    {
        return CallPostService<bool>("IsTollFreeMonth", date);
    }

    public bool IsTollFreeDayOfWeek(DateTime date)
    {
        return CallPostService<bool>("IsTollFreeDayOfWeek", date);
    }

    public bool IsTollFreeDate(DateTime date)
    {
        return CallPostService<bool>("IsTollFreeDate", date);
    }

    public bool IsYearsValid(DateTime[] dates)
    {
        return CallPostService<bool>("IsYearsValid", dates);
    }

    public bool IsMovementRangeValid(DateTime[] dates)
    {
        return CallPostService<bool>("IsMovementRangeValid", dates);
    }

    public int GetRuleMaxTax()
    {
        return CallGetService<int>("GetRuleMaxTax");
    }

    public int GetRuleMovementIntervalInMinute()
    {
        return CallGetService<int>("GetRuleMovementIntervalInMinute");
    }

    private TResult CallPostService<TResult>(string method, object data)
    {
        var myContent = JsonConvert.SerializeObject(data);
        var buffer = Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var resp = _httpClient
            .PostAsync($"https://localhost:7119/OutsideDataStore/{method}", byteContent)
            .GetAwaiter()
            .GetResult();
        var content = JsonConvert.DeserializeObject<TResult>(resp.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        return content;
    }

    private TResult CallGetService<TResult>(string method)
    {
        var resp = _httpClient
            .GetStringAsync($"https://localhost:7119/OutsideDataStore/{method}")
            .GetAwaiter()
            .GetResult();
        var content = JsonConvert.DeserializeObject<TResult>(resp);
        return content;
    }
}