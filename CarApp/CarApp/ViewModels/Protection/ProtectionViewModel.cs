using CarApp.Models;
using CarApp.Repositories;
using CarApp.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace CarApp.ViewModels
{
    public class ProtectionViewModel : BaseViewModel
    {
        private Protection _selectedItem;
        private bool isSort;
        public bool IsSort
        {
            get => isSort;
            set => SetProperty(ref isSort, value);

        }

        private string protName;
        public string ProtectionName
        {
            get => protName;
            set
            {
                SetProperty(ref protName, value);
            }
        }
        private long price;
        public long ProtectionPrice
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public ObservableCollection<Protection> Items { get; set; } = new ObservableCollection<Protection>();
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Protection> ItemTapped { get; }
        public Command SortListCommand { get; }

        public ProtectionViewModel()
        {
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Protection>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            SortListCommand = new Command(SortList);
        }

        async Task ExecuteLoadItemsCommand()
        {
            ProtectionPrice = 0;
            Items.Clear();
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var refills = await unitOfwork.ProtectionTables.Get<Protection>();
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
                var sorted = Items.OrderByDescending(x => x.ProtectionDate).ToList();
                Items.Clear();
                foreach (var item in sorted)
                {
                    Items.Add(item);
                }
            }
            else
            {
                isSort = false;
                var sorted = Items.OrderBy(x => x.ProtectionDate).ToList();
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

        public Protection SelectedItem
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
            await Shell.Current.GoToAsync(nameof(NewProtection));
        }

        async void OnItemSelected(Protection item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(PotectionDetailPage)}?{nameof(ProtectionDetailViewModel.ItemId)}={item.Id}");
        }
    }
}
