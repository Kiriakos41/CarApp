using CarApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewDamagePage : ContentPage
    {
        public NewDamagePage()
        {
            InitializeComponent();
            BindingContext = new NewDamageViewModel();
        }
    }
}