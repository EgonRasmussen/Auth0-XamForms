using Auth0XamForms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Auth0XamForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel vm;

        public LoginPage()
        {
            InitializeComponent();

            BindingContext = vm = new LoginViewModel();
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            vm.LoginCommand.Execute(null);
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            vm.LogoutCommand.Execute(null);
        }
    }
}