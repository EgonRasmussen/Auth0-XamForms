using Auth0XamForms.Auth;
using Auth0XamForms.Models;
using Auth0XamForms.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Auth0XamForms.ViewModels
{
    public class WeatherForecastViewModel : BaseViewModel
    {
        HttpClient _client;
        HttpClientHandler httpClientHandler = new HttpClientHandler();
        
        public ObservableCollection<WeatherForecast> WeatherForecasts { get; }
        public Command LoadWeatherForecastsCommand { get; }

        public WeatherForecastViewModel()
        {
            Title = "WeatherForecasts";
            WeatherForecasts = new ObservableCollection<WeatherForecast>();
            LoadWeatherForecastsCommand = new Command(async () => await ExecuteLoadWeatherForecastsCommand());
            

#if DEBUG
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, certificate, chain, sslPolicyErrors) => true;
#endif
            _client = new HttpClient(httpClientHandler);
        }

        async Task ExecuteLoadWeatherForecastsCommand()
        {
            IsBusy = true;
            var baseAddress = "https://10.0.2.2:5000";
            var uri = new Uri($"{baseAddress}/WeatherForecast");

            var accessToken = await SecureStorage.GetAsync("accessToken");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                Debug.WriteLine("************* Status code: " + response.StatusCode);

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
