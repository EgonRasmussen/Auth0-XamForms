using Auth0.OidcClient;
using Auth0XamForms.Auth;
using Auth0XamForms.Droid.Services;
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
        public AuthenticationResult AuthenticationResult { get; private set; }

        public async Task<AuthenticationResult> Authenticate()
        {
            var auth0LoginResult = await _auth0Client.LoginAsync();// (new { audience = AuthConfig.Audience });
            AuthenticationResult authenticationResult;

            if (!auth0LoginResult.IsError)
            {
                authenticationResult = new AuthenticationResult()
                {
                    AccessToken = auth0LoginResult.AccessToken,
                    IdToken = auth0LoginResult.IdentityToken,
                    UserClaims = auth0LoginResult.User.Claims
                };
            }
            else
                authenticationResult = new AuthenticationResult(auth0LoginResult.IsError, auth0LoginResult.Error);

            AuthenticationResult = authenticationResult;
            return authenticationResult;
        }

        public async Task Logout()
        {
            await _auth0Client.LogoutAsync();
        }
    }
}
