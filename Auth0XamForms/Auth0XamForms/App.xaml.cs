
using Xamarin.Forms;

namespace Auth0XamForms;

public partial class App : Application
{
    public static bool IsInReadRole { get; set; } = false;
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    protected override void OnStart()
    {
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }
}
