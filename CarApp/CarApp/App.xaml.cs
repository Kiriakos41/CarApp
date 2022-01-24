using CarApp.Models;
using SQLite;
using System.Globalization;
using Xamarin.Forms;

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
            DbPath.CreateTableAsync<Distance>();
            DbPath.CreateTableAsync<Damage>();
            DbPath.CreateTableAsync<Service>();
            DbPath.CreateTableAsync<AboutCar>();
            DbPath.CreateTableAsync<Rubber>();
            DbPath.CreateTableAsync<Extra>();
            DbPath.CreateTableAsync<Kteo>();
            DbPath.CreateTableAsync<Profile>();
            DbPath.CreateTableAsync<Protection>();
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
