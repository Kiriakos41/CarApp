using CarApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProtectionPage : ContentPage
    {
        ProtectionViewModel mb;
        public ProtectionPage()
        {
            InitializeComponent();
            BindingContext = mb = new ProtectionViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            mb.OnAppearingAsync();
        }
    }
}