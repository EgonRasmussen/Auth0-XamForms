using Auth0XamForms.ViewModels;

namespace Auth0XamForms
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        WeatherForecastViewModel vm;
        public AppShell()
        {
            InitializeComponent();
            vm = new WeatherForecastViewModel();
            BindingContext = vm;
        }
    }
}
