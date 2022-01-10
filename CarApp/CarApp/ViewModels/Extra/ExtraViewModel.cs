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
        private bool isSort;
        public bool IsSort
        {
            get => isSort;
            set => SetProperty(ref isSort, value);

        }

        private DateTime date;
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

            SortListCommand = new Command(SortList);

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
                Items.Clear();
                foreach (var item in sorted)
                {
                    Items.Add(item);
                }
            }
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