using Auth0XamForms.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Auth0XamForms.ViewModels
{
    public class WeatherForecastViewModel : BaseViewModel
    {
        HttpClient _client;
        public ObservableCollection<WeatherForecast> WeatherForecasts { get; }
        public Command LoadWeatherForecastsCommand { get; }

        public WeatherForecastViewModel()
        {
            Title = "WeatherForecasts";
            WeatherForecasts = new ObservableCollection<WeatherForecast>();
            LoadWeatherForecastsCommand = new Command(async () => await ExecuteLoadWeatherForecastsCommand());

            _client = new HttpClient();
        }

        async Task ExecuteLoadWeatherForecastsCommand()
        {
            IsBusy = true;
            var baseAddress = "https://192.168.1.23:45469";
            var uri = new Uri($"{baseAddress}/WeatherForecast");

            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<WeatherForecast>>(content);

                    WeatherForecasts.Clear();
                    foreach (var weatherForecast in data)
                    {
                        WeatherForecasts.Add(weatherForecast);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            IsBusy = false;
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
