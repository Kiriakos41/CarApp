using CarApp.ViewModels;
using Xamarin.Forms;

namespace CarApp.Views
{
    public partial class ItemsPage : ContentPage
    {
        AboutCarViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new AboutCarViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}