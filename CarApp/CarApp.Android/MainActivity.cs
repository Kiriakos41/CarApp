using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.IO;
using Environment = System.Environment;
using SQLite;
using Plugin.LocalNotification;
namespace CarApp.Droid
{
    [Activity(Label = "AutoCar",
        Icon = "@mipmap/TutIcon",
        Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            NotificationCenter.CreateNotificationChannel();

            var filename = "Car12345.sqlite";
            var fileLocation = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var fullpath = Path.Combine(fileLocation, filename);

            var conn = new SQLiteAsyncConnection(fullpath);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(conn));


        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}