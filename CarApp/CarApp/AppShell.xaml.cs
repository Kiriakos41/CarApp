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
            Routing.RegisterRoute(nameof(DamageDetailPage), typeof(DamageDetailPage));
            Routing.RegisterRoute(nameof(NewDamagePage), typeof(NewDamagePage));
            Routing.RegisterRoute(nameof(ChartsPage), typeof(ChartsPage));
            Routing.RegisterRoute(nameof(CarPage), typeof(CarPage));
            Routing.RegisterRoute(nameof(RubberPage), typeof(RubberPage));
            Routing.RegisterRoute(nameof(NewRubberPage), typeof(NewRubberPage));
            Routing.RegisterRoute(nameof(RubberDetailPage), typeof(RubberDetailPage));
            Routing.RegisterRoute(nameof(NewKteoPage), typeof(NewKteoPage));
            Routing.RegisterRoute(nameof(KteosPage), typeof(KteosPage));
            Routing.RegisterRoute(nameof(KteoDetailPage), typeof(KteoDetailPage));
            Routing.RegisterRoute(nameof(ExtraDetailPage), typeof(ExtraDetailPage));
            Routing.RegisterRoute(nameof(ExtraPage), typeof(ExtraPage));
            Routing.RegisterRoute(nameof(NewExtraPage), typeof(NewExtraPage));
            Routing.RegisterRoute(nameof(testPage), typeof(testPage));
        }

    }
}
