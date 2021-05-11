using System;
using System.Threading.Tasks;
using MsBlazorServerPlayGround.Data;

namespace MsBlazorServerPlayGround.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
    }
}