using Auth0XamForms.Auth;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using System.Collections.Generic;
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
            Scope = "openid profile email api offline_access todo:write",
            RedirectUri = $"com.companyname.auth0xamforms://eucsyd.eu.auth0.com/android/com.companyname.auth0xamforms/callback",
            Browser = browser
        };

        _client = new OidcClient(options);
    }

    Command loginCommand;
    public Command LoginCommand => loginCommand ??= new Command(async () =>
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
    public Command LogoutCommand => logoutCommand ??= new Command(() =>
        {
            SecureStorage.Remove("accessToken");
            IsLoggedIn = false;
        });
}
