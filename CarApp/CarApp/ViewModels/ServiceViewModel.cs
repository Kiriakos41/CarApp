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
    public class ServiceViewModel : BaseViewModel
    {
        private Service _selectedItem;
        public ObservableCollection<Service> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Service> ItemTapped { get; }
        public Command SortListCommand { get; }
        public long Pr
        {
            get => pr;
            set => SetProperty(ref pr, value);
        }
        private long pr;

        private bool isSort;
        public bool IsSort
        {
            get => isSort;
            set => SetProperty(ref isSort, value);

        }
        public ServiceViewModel()
        {
            Items = new ObservableCollection<Service>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Service>(OnItemSelected);

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
                Pr = 0;
                foreach (var item in sorted)
                {
                    Items.Add(item);
                    Pr += Convert.ToInt64(item.Price);
                }
            }
            else
            {
                isSort = false;
                var sorted = Items.OrderBy(x => x.Price).ToList();
                Items.Clear();
                Pr = 0;
                foreach (var item in sorted)
                {
                    Items.Add(item);
                    Pr += Convert.ToInt64(item.Price);
                }
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                Pr = 0;
                Items.Clear();
                var items = await ServiceData.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                    Pr += Convert.ToInt64(item.Price);
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

        public Service SelectedItem
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
            await Shell.Current.GoToAsync(nameof(NewServicePage));
        }

        async void OnItemSelected(Service item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ServiceDetailPage)}?{nameof(ServiceDetailViewModel.ItemId)}={item.Id}");
        }
    }
}
