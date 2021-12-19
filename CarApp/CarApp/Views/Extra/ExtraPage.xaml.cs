using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarApp.ViewModels;

namespace CarApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExtraPage : ContentPage
    {
        ExtraViewModel extraViewModel;
        public ExtraPage()
        {
            InitializeComponent();
            BindingContext = extraViewModel = new ExtraViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            extraViewModel.OnAppearing();
        }
    }
}