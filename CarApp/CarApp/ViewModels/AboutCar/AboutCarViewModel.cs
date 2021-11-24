using CarApp.Models;
using CarApp.Repositories;
using CarApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    public class AboutCarViewModel : BaseViewModel
    {

        private AboutCar _selectedItem;
        public ObservableCollection<AboutCar> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command DeleteCommand { get; }
        public Command<AboutCar> ItemTapped { get; }
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
        private bool isSort;
        public bool IsSort
        {
            get => isSort;
            set => SetProperty(ref isSort, value);

        }
        public AboutCarViewModel()
        {
            Title = "Γεμίσματα";
            Items = new ObservableCollection<AboutCar>();


            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<AboutCar>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            SortListCommand = new Command(SortList);

            DeleteCommand = new Command(DeleteItem);
        }
        public void SortList()
        {
            if (!isSort)
            {
                SortImage = "sortdescending";
                SortText = "Ταξινόμηση από το παλιότερο προς το νεότερο: ";
                isSort = true;
                var sorted = Items.OrderByDescending(x => x.Date).ToList();
                Items.Clear();
                foreach (var item in sorted)
                {
                    Items.Add(item);
                }
            }
            else
            {
                SortImage = "sortascending";
                SortText = "Ταξινόμηση από το νεότερο προς το παλιότερο: ";
                isSort = false;
                var sorted = Items.OrderBy(x => x.Date).ToList();
                Items.Clear();
                foreach (var item in sorted)
                {
                    Items.Add(item);
                }
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            Items.Clear();
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var refills = await unitOfwork.AboutCars.Get<AboutCar>();
                foreach (var refil in refills)
                {
                    Items.Add(refil);
                }
                /////////////
                SortList();
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public AboutCar SelectedItem
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
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(AboutCar item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
        private async void DeleteItem()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                //await unitOfwork.AboutCars.Delete();
            }

        }
    }
}