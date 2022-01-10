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
    public class RubberViewModel : BaseViewModel
    {
        private Rubber _selectedItem;
        public ObservableCollection<Rubber> RuberItems { get; set; }
        public Command AddItemCommand { get; }
        public Command<Rubber> ItemTapped { get; }
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
        public RubberViewModel()
        {
            RuberItems = new ObservableCollection<Rubber>();

            ItemTapped = new Command<Rubber>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            SortListCommand = new Command(SortList);
        }

        public async void fillList()
        {
            RuberItems.Clear();
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var servs = await unitOfwork.RubberTables.Get<Service>();
                foreach (var srv in servs)
                {
                    RuberItems.Add(srv);
                }
            }
            IsBusy = false;
        }

        public void SortList()
        {
            if (!isSort)
            {
                isSort = true;
                var sorted = RuberItems.OrderByDescending(x => x.Date).ToList();
                RuberItems.Clear();
                foreach (var item in sorted)
                {
                    RuberItems.Add(item);
                }
            }
            else
            {
                isSort = false;
                var sorted = RuberItems.OrderBy(x => x.Date).ToList();
                RuberItems.Clear();
                foreach (var item in sorted)
                {
                    RuberItems.Add(item);
                }
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Rubber SelectedItem
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
            await Shell.Current.GoToAsync(nameof(NewRubberPage));
        }

        async void OnItemSelected(Rubber item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(RubberDetailPage)}?{nameof(RubberDetailViewModel.ItemId)}={item.Id}");
        }
    }
}