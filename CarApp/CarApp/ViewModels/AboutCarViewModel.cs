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
        public long Pr
        {
            get => pr;
            set => SetProperty(ref pr, value);
        }
        private long pr;
        private long kil;
        private long gs;
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
                isSort = true;
                var sorted = Items.OrderByDescending(x => x.Kilometer).ToList();
                Items.Clear();
                Gs = 0;
                Pr = 0;
                Kil = 0;
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
                isSort = false;
                var sorted = Items.OrderBy(x => x.Kilometer).ToList();
                Items.Clear();
                Gs = 0;
                Pr = 0;
                Kil = 0;
                foreach (var item in sorted)
                {
                    Items.Add(item);
                    Pr += Convert.ToInt64(item.Price);
                    Gs += Convert.ToInt64(item.Gas);
                    Kil += Convert.ToInt64(item.Kilometer);
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
                Kil = 0;
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                    Pr += Convert.ToInt64(item.Price);
                    Gs += Convert.ToInt64(item.Gas);
                    Kil += Convert.ToInt64(item.Kilometer);
                }

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