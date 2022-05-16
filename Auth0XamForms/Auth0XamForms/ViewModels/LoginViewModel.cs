using Auth0XamForms.Auth;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Auth0XamForms.ViewModels;

public class LoginViewModel : BaseViewModel
{
    OidcClient _client;
    LoginResult _result;

    public LoginViewModel()
    {
        var browser = DependencyService.Get<IBrowser>();
        var options = new OidcClientOptions
        {
            Authority = AuthConfig.Domain,
            ClientId = AuthConfig.ClientId,
            Scope = AuthConfig.Scopes,
            RedirectUri = AuthConfig.RedirectUri,
            Browser = browser
        };

        _client = new OidcClient(options);
    }

    Command loginCommand;
    public ICommand LoginCommand => loginCommand ??= new Command(async () =>
        {
            // Nødvendigt at sende audience (aud) med for at få en AccessToken i form af en JWT
            var audience = new Dictionary<string, string>
            {
                { "audience", AuthConfig.Audience }
            };

            var loginRequest = new LoginRequest() { FrontChannelExtraParameters = new Parameters(audience) };

            try
            {
                _result = await _client.LoginAsync(loginRequest);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            if (!_result.IsError)
            {
                await SecureStorage.SetAsync("accessToken", _result.AccessToken);
                IsLoggedIn = true;
            }

            await Shell.Current.GoToAsync($"//WeatherForecast");
        });

    Command logoutCommand;
    public ICommand LogoutCommand => logoutCommand ??= new Command(async () =>
        {
            SecureStorage.Remove("accessToken");
            IsLoggedIn = false;
            await Shell.Current.GoToAsync($"//About");
        });
}
