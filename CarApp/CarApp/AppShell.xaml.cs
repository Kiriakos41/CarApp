using CarApp.ViewModels;
using CarApp.Views;
using Xamarin.Forms;

namespace CarApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = new MyCarViewModel();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
            Routing.RegisterRoute(nameof(CleanPage), typeof(CleanPage));
            Routing.RegisterRoute(nameof(ServicePage), typeof(ServicePage));
            Routing.RegisterRoute(nameof(NewCleanPage), typeof(NewCleanPage));
            Routing.RegisterRoute(nameof(CleanDetailPage), typeof(CleanDetailPage));
            Routing.RegisterRoute(nameof(ServicePage), typeof(ServicePage));
            Routing.RegisterRoute(nameof(NewServicePage), typeof(NewServicePage));
            Routing.RegisterRoute(nameof(ServiceDetailPage), typeof(ServiceDetailPage));
            Routing.RegisterRoute(nameof(DamagePage), typeof(DamagePage));
        }

    }
}
