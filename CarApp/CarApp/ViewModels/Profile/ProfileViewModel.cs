using CarApp.Models;
using CarApp.Repositories;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;

namespace CarApp.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public Command NewImage { get; }
        public Command LoadImg { get; }
        public Command Email { get; }
        private string profileImage;
        public ProfileViewModel()
        {
            NewImage = new Command(AddImage);
            LoadImg = new Command(LoadImage);
            Email = new Command(CopyEmailAsync);
        }
        public string ProfileImage
        {
            get => profileImage;
            set => SetProperty(ref profileImage, value);
        }
        public string PhotoPath { get; set; }
        public async void AddImage()
        {
            var file = await MediaPicker.PickPhotoAsync();
            await LoadPhotoAsync(file);
            if (file == null)
                return;

            async Task LoadPhotoAsync(FileResult photo)
            {
                // canceled
                if (photo == null)
                {
                    PhotoPath = null;
                    return;
                }
                // save the file into local storage

                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);
                PhotoPath = newFile;

                using (var unitOfwork = new UnitOfWork(App.DbPath))
                {
                    var imageUri = await unitOfwork.ProfileTb.Get<Profile>();

                    Profile newProfile = new Profile()
                    {
                        ImagePath = newFile,
                    };

                    await unitOfwork.ProfileTb.Insert(newProfile);
                }
                LoadImage();
            }
        }
        public async void LoadImage()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var imageUri = await unitOfwork.ProfileTb.Get<Profile>();
                if (imageUri.Count == 0)
                {
                    ProfileImage = "NoImage.jpg";
                }
                else
                {
                    var uri = imageUri.Last().ImagePath;
                    ProfileImage = uri;
                }

            }
        }
        public void OnAppearing()
        {
            IsBusy = true;
            LoadImage();
        }
        public async void CopyEmailAsync()
        {
            await Clipboard.SetTextAsync("kiriakos41@outlook.com");
            await App.Current.MainPage.DisplayAlert("Ενημέρωση", "Έγινε αντιγραφή του email", "Εντάξει");
        }
    }
}
