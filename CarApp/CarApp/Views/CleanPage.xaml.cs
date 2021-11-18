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
    public partial class CleanPage : ContentPage
    {
        CleanViewModel ViewModel;
        public CleanPage()
        {
            InitializeComponent();

            BindingContext = ViewModel =  new CleanViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
    }
}