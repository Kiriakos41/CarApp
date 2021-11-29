using CarApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewDamagePage : ContentPage
    {
        NewDamageViewModel vm;
        public NewDamagePage()
        {
            InitializeComponent();
            BindingContext = vm = new NewDamageViewModel();
        }
    }
}