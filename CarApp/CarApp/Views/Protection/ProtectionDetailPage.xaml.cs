using CarApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PotectionDetailPage : ContentPage
    {
        public PotectionDetailPage()
        {
            InitializeComponent();
            BindingContext = new ProtectionDetailViewModel();
        }
    }
}