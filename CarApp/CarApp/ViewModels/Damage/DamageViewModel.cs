using CarApp.Models;
using CarApp.Repositories;
using CarApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace CarApp.ViewModels
{
    public class DamageViewModel : BaseViewModel
    {
        private Damage _selectedItem;

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
        public ObservableCollection<Damage> Items { get; set; } = new ObservableCollection<Damage>();
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Damage> ItemTapped { get; }
        public Command SortListCommand { get; }

        public DamageViewModel()
        {
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Damage>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            Price = 0;
            Items.Clear();
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var refills = await unitOfwork.Damages.Get<Damage>();
                foreach (var refil in refills)
                {
                    Items.Add(refil);
                }
            }
            IsBusy = false;
        }

        public void OnAppearingAsync()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Damage SelectedItem
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
            await Shell.Current.GoToAsync(nameof(NewDamagePage));
        }

        async void OnItemSelected(Damage item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(DamageDetailPage)}?{nameof(DamageDetailViewModel.ItemId)}={item.Id}");
        }
    }
}
