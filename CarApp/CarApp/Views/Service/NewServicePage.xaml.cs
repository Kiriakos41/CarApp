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
    public partial class NewServicePage : ContentPage
    {
        NewServiceViewModel viewModel;
        public NewServicePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new NewServiceViewModel();
        }
    }
}