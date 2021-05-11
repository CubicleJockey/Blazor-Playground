using System;
using System.Threading.Tasks;

namespace MsBlazorServerPlayGround.Data
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
    }
}