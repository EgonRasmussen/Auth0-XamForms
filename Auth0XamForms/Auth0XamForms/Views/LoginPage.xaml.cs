using Auth0XamForms.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Auth0XamForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
    }
}