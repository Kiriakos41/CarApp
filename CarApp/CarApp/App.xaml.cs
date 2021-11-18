using CarApp.Services;
using CarApp.ViewModels;
using CarApp.Views;
using System;
using System.Globalization;
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
            SetCultureToGreek();
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
        private void SetCultureToGreek()
        {
            CultureInfo englishUSCulture = new CultureInfo("el-Gr");
            CultureInfo.DefaultThreadCurrentCulture = englishUSCulture;
        }
    }
}
