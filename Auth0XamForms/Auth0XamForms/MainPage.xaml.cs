using IdentityModel.Client;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Xamarin.Forms;

namespace Auth0XamForms
{
    public partial class MainPage : ContentPage
    {
        OidcClient _client;
        LoginResult _result;

        Lazy<HttpClient> _apiClient = new Lazy<HttpClient>(() => new HttpClient());

        public MainPage()
        {
            InitializeComponent();

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
            _apiClient.Value.BaseAddress = new Uri(AuthConfig.WebApi);
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            // Ved brug af Auth0: Nødvendigt at sende audience (aud) med for at få en AccessToken i form af en JWT
            var audience = new Dictionary<string, string>
            {
                { "audience", AuthConfig.Audience }
            };

            var loginRequest = new LoginRequest() { FrontChannelExtraParameters = new Parameters(audience) };


            _result = await _client.LoginAsync(loginRequest);

            if (_result.IsError)
            {
                OutputText.Text = _result.Error;
                return;
            }

            var sb = new StringBuilder(128);
            foreach (var claim in _result.User.Claims)
            {
                sb.AppendFormat("{0}: {1}\n", claim.Type, claim.Value);
            }

            sb.AppendFormat("\n{0}: {1}\n", "refresh token", _result?.RefreshToken ?? "none");
            sb.AppendFormat("\n{0}: {1}\n", "access token", _result.AccessToken);

            OutputText.Text = sb.ToString();
            System.Diagnostics.Debug.WriteLine(sb.ToString());

            _apiClient.Value.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _result?.AccessToken ?? "");
        }

        private async void CallApi_Clicked(object sender, EventArgs e)
        {

            var result = await _apiClient.Value.GetAsync("api/test");

            if (result.IsSuccessStatusCode)
            {
                OutputText.Text = JsonDocument.Parse(await result.Content.ReadAsStringAsync()).RootElement.GetRawText();
            }
            else
            {
                OutputText.Text = result.ReasonPhrase;
            }
        }
    }
}
