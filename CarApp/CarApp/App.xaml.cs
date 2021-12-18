
using CarApp.Models;
using CarApp.ViewModels;
using CarApp.Views;
using SQLite;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarApp
{
    public partial class App : Application
    {
        public static SQLiteAsyncConnection DbPath;
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            SetCultureToGreek();
        }
        public App(SQLiteAsyncConnection dbPath) : this()
        {
            DbPath = dbPath;
        }

        protected override void OnStart()
        {
            DbPath.CreateTableAsync<Damage>();
            DbPath.CreateTableAsync<Service>();
            DbPath.CreateTableAsync<AboutCar>();
            DbPath.CreateTableAsync<Clean>();
            DbPath.CreateTableAsync<Rubber>();
            DbPath.CreateTableAsync<Extra>();
            DbPath.CreateTableAsync<Kteo>();
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
