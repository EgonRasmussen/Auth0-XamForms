using Auth0XamForms.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Auth0XamForms.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _authenticationService;

        public LoginViewModel()
        {
            _authenticationService = DependencyService.Get<IAuthService>();
        }

        Command loginCommand;
        public Command LoginCommand => loginCommand
            ?? (loginCommand = new Command(async () =>
            {
                AuthenticationResult authenticationResult = await _authenticationService.Authenticate();
                if (!authenticationResult.IsError)
                {
                    await SecureStorage.SetAsync("accessToken", authenticationResult.AccessToken);
                    IsLoggedIn = true;
                }

                await Shell.Current.GoToAsync($"//WeatherForecast");
            }));

        Command logoutCommand;
        public Command LogoutCommand => logoutCommand
            ?? (logoutCommand = new Command(async () =>
            {
                await _authenticationService.Logout();
                SecureStorage.Remove("accessToken");
                IsLoggedIn = false;
            }));
    }
}
