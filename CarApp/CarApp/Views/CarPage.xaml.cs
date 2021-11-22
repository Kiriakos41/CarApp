using CarApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarPage : ContentPage
    {
        MyCarViewModel viewModel;
        public CarPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MyCarViewModel();
        }
        protected override void OnAppearing()
        {
            viewModel.OnAppearAsync();
        }
    }
}