using IdentityModel.OidcClient;
using System.Threading.Tasks;

namespace Auth0XamForms.Auth
{
    public interface IAuthService
    {
        Task<LoginResult> Authenticate();
        Task Logout();
    }
}
