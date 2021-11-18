using CarApp.Models;
using CarApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    public class CleanViewModel : BaseViewModel
    {
        private Clean _selectedItem;
        private bool isSort;
        public bool IsSort
        {
            get => isSort;
            set => SetProperty(ref isSort, value);

        }

        private string quality;
        public string Quality
        {
            get => quality;
            set
            {
                SetProperty(ref quality, value);
            }
        }
        private long price;
        public long Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public ObservableCollection<Clean> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Clean> ItemTapped { get; }
        public Command SortListCommand { get; }

        public CleanViewModel()
        {
            Items = new ObservableCollection<Clean>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Clean>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            SortListCommand = new Command(SortList);
        }
        public void SortList()
        {
            if (!isSort)
            {
                isSort = true;
                var sorted = Items.OrderByDescending(x => x.Price).ToList();
                Items.Clear();
                Price = 0;
                foreach (var item in sorted)
                {
                    Items.Add(item);
                    Price += Convert.ToInt64(item.Price);
                }
            }
            else
            {
                isSort = false;
                var sorted = Items.OrderBy(x => x.Price).ToList();
                Items.Clear();
                Price = 0;
                foreach (var item in sorted)
                {
                    Items.Add(item);
                    Price += Convert.ToInt64(item.Price);
                }
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                Price = 0;
                Items.Clear();
                var items = await CleanData.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                    Price += Convert.ToInt64(item.Price);
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

        public Clean SelectedItem
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
            await Shell.Current.GoToAsync(nameof(NewCleanPage));
        }

        async void OnItemSelected(Clean item)
        {
            if (item == null)
                return;

             await Shell.Current.GoToAsync($"{nameof(CleanDetailPage)}?{nameof(CleanDetailViewModel.ItemId)}={item.Id}");
        }
    }
}
