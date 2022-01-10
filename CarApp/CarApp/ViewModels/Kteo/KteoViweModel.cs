using CarApp.Models;
using CarApp.Repositories;
using CarApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    public class KteoViweModel : BaseViewModel
    {
        private Kteo _selectedItem;

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
        public ObservableCollection<Kteo> Items { get; set; } = new ObservableCollection<Kteo>();
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Kteo> ItemTapped { get; }
        public Command SortListCommand { get; }

        private bool isSort;
        public bool IsSort
        {
            get => isSort;
            set => SetProperty(ref isSort, value);

        }
        public KteoViweModel()
        {
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Kteo>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            SortListCommand = new Command(SortList);
        }

        async Task ExecuteLoadItemsCommand()
        {
            Price = 0;
            Items.Clear();
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var refills = await unitOfwork.KteoTables.Get<Kteo>();
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

        public void OnAppearingAsync()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Kteo SelectedItem
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
            await Shell.Current.GoToAsync(nameof(NewKteoPage));
        }

        async void OnItemSelected(Kteo item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(KteoDetailPage)}?{nameof(KteoDetailViewModel.ItemId)}={item.Id}");
        }
    }
}
