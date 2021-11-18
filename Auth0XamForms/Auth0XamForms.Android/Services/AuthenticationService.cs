using Auth0.OidcClient;
using Auth0XamForms.Auth;
using Auth0XamForms.Droid.Services;
using IdentityModel.OidcClient;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthenticationService))]
namespace Auth0XamForms.Droid.Services
{
    public class AuthenticationService : IAuthService
    {
        private Auth0Client _auth0Client;

        public AuthenticationService()
        {
            _auth0Client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = AuthConfig.Domain,
                ClientId = AuthConfig.ClientId
            });
        }

        public Task<LoginResult> Authenticate()
        {
            return _auth0Client.LoginAsync();
        }

        public async Task Logout()
        {
            await _auth0Client.LogoutAsync();
        }
    }
}
