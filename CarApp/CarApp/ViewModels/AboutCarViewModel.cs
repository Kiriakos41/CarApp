using CarApp.Models;
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
        public Command<AboutCar> ItemTapped { get; }
        public Command SortListCommand { get; }

        private long pr;
        private long kil;
        private long gs;
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
        public long Pr
        {
            get => pr;
            set => SetProperty(ref pr, value);
        }
        public DateTime Date
        {
            get =>  date;
            set => SetProperty(ref date, value);
        }
        public long Kil
        {
            get => kil;
            set => SetProperty(ref kil, value);
        }
        private bool isSort;
        public bool IsSort
        {
            get => isSort;
            set => SetProperty(ref isSort, value);

        }
        public long Gs
        {
            get => gs;
            set => SetProperty(ref gs, value);
        }
        public AboutCarViewModel()
        {
            Title = "Γεμίσματα";
            Items = new ObservableCollection<AboutCar>();


            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<AboutCar>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            SortListCommand = new Command(SortList);
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
                Gs = 0;
                Pr = 0;
                foreach (var item in sorted)
                {
                    Items.Add(item);
                    Pr += Convert.ToInt64(item.Price);
                    Gs += Convert.ToInt64(item.Gas);
                    Kil += Convert.ToInt64(item.Kilometer);
                }
            }
            else
            {
                SortImage = "sortascending";
                SortText = "Ταξινόμηση από το νεότερο προς το παλιότερο: ";
                isSort = false;
                var sorted = Items.OrderBy(x => x.Date).ToList();
                Items.Clear();
                Gs = 0;
                Pr = 0;
                foreach (var item in sorted)
                {
                    Items.Add(item);
                    Pr += Convert.ToInt64(item.Price);
                    Gs += Convert.ToInt64(item.Gas);
                }
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                Gs = 0;
                Pr = 0;
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                var sortedItems = items.OrderBy(x => x.Date).ToList();
                foreach (var item in sortedItems)
                {
                    Items.Add(item);
                    Pr += Convert.ToInt64(item.Price);
                    Gs += Convert.ToInt64(item.Gas);
                }
                SortList();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
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
    }
}