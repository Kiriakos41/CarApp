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
    public partial class KteosPage : ContentPage
    {
        KteoViweModel model;
        public KteosPage()
        {
            InitializeComponent();
            BindingContext = model = new KteoViweModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            model.OnAppearingAsync();
        }
    }
}