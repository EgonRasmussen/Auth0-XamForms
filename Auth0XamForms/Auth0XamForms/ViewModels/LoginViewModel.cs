using Auth0XamForms.Auth;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using System.Collections.Generic;
using System.Text.Encodings.Web;
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
            Authority = $"https://{AuthConfig.Domain}",
            ClientId = AuthConfig.ClientId,
            Scope = AuthConfig.Scopes,
            RedirectUri = $"{AuthConfig.PackageName}://{AuthConfig.Domain}/android/{AuthConfig.PackageName}/callback",
            PostLogoutRedirectUri = AuthConfig.PostLogoutRedirectUri,           
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
                await SecureStorage.SetAsync("identityToken", _result.IdentityToken);
                IsLoggedIn = true;
            }

            await Shell.Current.GoToAsync($"//WeatherForecast");
        });

    Command logoutCommand;
    public ICommand LogoutCommand => logoutCommand ??= new Command(async () =>
        {
            // Med credit til Sofus S.
            var url = $"https://{AuthConfig.Domain}/v2/logout?client_id={AuthConfig.ClientId}&returnTo={UrlEncoder.Default.Encode(_client.Options.PostLogoutRedirectUri)}";
            var browserOptions = new BrowserOptions(url, _client.Options.PostLogoutRedirectUri ?? string.Empty);
            var res = await _client.Options.Browser.InvokeAsync(browserOptions);

            if (!res.IsError)
            {
                SecureStorage.Remove("accessToken");
                IsLoggedIn = false;
                System.Console.WriteLine("Du er nu logget ud og AccessToken er slettet!");
                await Shell.Current.GoToAsync($"//About");
            }      
        });
}
