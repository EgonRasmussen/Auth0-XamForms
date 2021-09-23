using Auth0XamForms.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Auth0XamForms.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}