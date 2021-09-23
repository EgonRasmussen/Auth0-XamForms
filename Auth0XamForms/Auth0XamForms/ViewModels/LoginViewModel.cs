using Auth0XamForms.Auth;
using Auth0XamForms.Views;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Auth0XamForms.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _authenticationService;

        public ICommand LoginCommand { get; }
        public ICommand LogOutCommand { get; }

        public LoginViewModel()
        {
            _authenticationService = DependencyService.Get<IAuthService>();
            LoginCommand = new Command(OnLoginClicked);
            LogOutCommand = new Command(OnLogoutClicked);
        }

        private bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set => SetProperty(ref _isLoggedIn, value);
        }

        private async void OnLoginClicked()
        {
            var authenticationResult = await _authenticationService.Authenticate();
            if (!authenticationResult.IsError)
            {
                await SecureStorage.SetAsync("accessToken", authenticationResult.AccessToken);
                IsLoggedIn = true;
            }

            await Shell.Current.GoToAsync($"//{nameof(WeatherForecastPage)}");
        }

        private async void OnLogoutClicked()
        {
            await _authenticationService.Logout();
            SecureStorage.Remove("accessToken");
            IsLoggedIn = false;
        }
    }
}
