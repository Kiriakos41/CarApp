using CarApp.ViewModels;
using Microcharts;
using SkiaSharp;
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
    public partial class ChartsPage : ContentPage
    {
       readonly ChartViewModel vm;
        public ChartsPage()
        {
            InitializeComponent();
            BindingContext = vm = new ChartViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            chartViewBar.Chart = new BarChart { Entries = vm.Entries };
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            chartViewBar.Chart = new BarChart { Entries = null };
            chartViewBar.Chart = new BarChart { Entries = vm.Entries };
        }
    }
}