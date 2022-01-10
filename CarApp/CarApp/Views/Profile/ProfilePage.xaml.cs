using CarApp.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        ProfileViewModel vm;
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = vm = new ProfileViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.OnAppearing();
        }
    }
}