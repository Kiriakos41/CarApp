using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewRubberPage : ContentPage
    {
        public NewRubberPage()
        {
            InitializeComponent();
            BindingContext = new NewRubberViewModel();
        }
    }
}