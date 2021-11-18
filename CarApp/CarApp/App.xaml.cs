using CarApp.Services;
using CarApp.ViewModels;
using CarApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<CarData>();
            DependencyService.Register<CleanData>();
            DependencyService.Register<ServiceData>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
