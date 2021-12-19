using CarApp.Models;
using CarApp.Repositories;
using CarApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    public class ExtraViewModel : BaseViewModel
    {
        private Extra _selectedItem;
        public ObservableCollection<Extra> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Extra> ItemTapped { get; }
        public Command SortListCommand { get; }

        private string sortText;
        private string sortImage;
        private DateTime date;

        public string SortText
        {
            get => sortText;
            set => SetProperty(ref sortText, value);
        }
        public string SortImage
        {
            get => sortImage;
            set => SetProperty(ref sortImage, value);
        }
        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public ExtraViewModel()
        {
            Title = "Γεμίσματα";
            Items = new ObservableCollection<Extra>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Extra>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            Items.Clear();
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var refills = await unitOfwork.ExtraTables.Get<AboutCar>();
                foreach (var refil in refills)
                {
                    Items.Add(refil);
                }
            }
            IsBusy = false;
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Extra SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewExtraPage));
        }

        async void OnItemSelected(Extra item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ExtraDetailPage)}?{nameof(ExtraDetailViewModel.ItemId)}={item.Id}");
        }
    }
}