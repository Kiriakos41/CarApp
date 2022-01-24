

using CarApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewProtection : ContentPage
    {
        public NewProtection()
        {
            InitializeComponent();
            BindingContext = new NewProtectionViewModel();
        }
    }
}