using System.Threading.Tasks;

namespace Auth0XamForms.Auth
{
    public interface IAuthService
    {
        Task<AuthenticationResult> Authenticate();
        Task Logout();
    }
}
