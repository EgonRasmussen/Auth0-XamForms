using Auth0XamForms.Auth;
using System;
using System.Text;
using Xamarin.Forms;

namespace Auth0XamForms
{
    public partial class MainPage : ContentPage
    {
        IAuthService authenticationService;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            authenticationService = DependencyService.Get<IAuthService>();
            var loginResult = await authenticationService.Authenticate();

            var sb = new StringBuilder();

            if (loginResult.IsError)
            {
                ResultLabel.Text = "An error occurred during login...";

                sb.AppendLine("An error occurred during login:");
                sb.AppendLine(loginResult.Error);
            }
            else
            {
                sb.AppendLine($"Welcome {loginResult.User.Identity.Name}");
                sb.AppendLine($"ID Token: {loginResult.IdentityToken}");
                sb.AppendLine();
                sb.AppendLine($"Access Token: {loginResult.AccessToken}");
                sb.AppendLine();
                sb.AppendLine($"Refresh Token: {loginResult.RefreshToken}");
                sb.AppendLine();
                sb.AppendLine("-- Claims --");
                foreach (var claim in loginResult.User.Claims)
                {
                    sb.AppendLine($"{claim.Type} = {claim.Value}");
                }
                sb.AppendLine();
                ResultLabel.Text = sb.ToString();
            }

            System.Diagnostics.Debug.WriteLine(sb.ToString());
        }

        private void LogoutButton_Clicked(object sender, EventArgs e)
        {
            authenticationService.Logout();
            ResultLabel.Text = $"You are logged out";
        }
    }
}
