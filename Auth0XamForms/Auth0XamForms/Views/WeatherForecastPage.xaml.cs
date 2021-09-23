using Auth0XamForms.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Auth0XamForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherForecastPage : ContentPage
    {
        WeatherForecastViewModel _viewModel;

        public WeatherForecastPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new WeatherForecastViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}