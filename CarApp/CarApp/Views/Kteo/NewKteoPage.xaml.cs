using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarApp.ViewModels;

namespace CarApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewKteoPage : ContentPage
    {
        public NewKteoPage()
        {
            InitializeComponent();
            BindingContext = new NewKteoViewModel();
        }
    }
}