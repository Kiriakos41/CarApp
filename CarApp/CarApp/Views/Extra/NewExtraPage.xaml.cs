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
    public partial class NewExtraPage : ContentPage
    {
        public NewExtraPage()
        {
            InitializeComponent();
            BindingContext = new NewExtraViewModel();
        }
    }
}