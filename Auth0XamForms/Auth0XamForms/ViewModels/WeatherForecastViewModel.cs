using Auth0XamForms.Auth;
using Auth0XamForms.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Auth0XamForms.ViewModels;

public class WeatherForecastViewModel : BaseViewModel
{
    HttpClient _httpClient;
    HttpClientHandler httpClientHandler = new HttpClientHandler();

    public ObservableCollection<WeatherForecast> WeatherForecasts { get; }

    public WeatherForecastViewModel()
    {
        Title = "WeatherForecasts";
        WeatherForecasts = new ObservableCollection<WeatherForecast>();

#if DEBUG
        httpClientHandler.ServerCertificateCustomValidationCallback = (message, certificate, chain, sslPolicyErrors) => true;
#endif
        _httpClient = new HttpClient(httpClientHandler);
    }

    private Command loadWeatherForecastsCommand;
    public ICommand LoadWeatherForecastsCommand => loadWeatherForecastsCommand ??= new Command(async () => await ExecuteLoadWeatherForecastsCommand());
    async Task ExecuteLoadWeatherForecastsCommand()
    {
        IsBusy = true;
        var uri = new Uri($"{AuthConfig.BaseUrl}/WeatherForecast");

        var accessToken = await SecureStorage.GetAsync("accessToken");

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            Debug.WriteLine("************* Status code: " + response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<WeatherForecast>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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
