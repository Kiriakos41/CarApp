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
    public class DistanceViewModel : BaseViewModel
    {
        private Distance _selectedItem;
        private bool isSort;
        private string quality;
        public bool IsSort
        {
            get => isSort;
            set => SetProperty(ref isSort, value);

        }
        public string Quality
        {
            get => quality;
            set
            {
                SetProperty(ref quality, value);
            }
        }
        public ObservableCollection<Distance> Items { get; set; } = new ObservableCollection<Distance>();
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Distance> ItemTapped { get; }
        public Command SortListCommand { get; }
        public DistanceViewModel()
        {
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Distance>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            SortListCommand = new Command(SortList);
        }
        public void SortList()
        {
            if (!isSort)
            {
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
                isSort = false;
                var sorted = Items.OrderBy(x => x.Date).ToList();
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
                var refills = await unitOfwork.DistanceTable.Get<Distance>();
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
        public Distance SelectedItem
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
            await Shell.Current.GoToAsync(nameof(NewDistancePage));
        }
        async void OnItemSelected(Distance item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(DistanceDetailPage)}?{nameof(DistanceDetailViewModel.ItemId)}={item.Id}");
        }
    }
}