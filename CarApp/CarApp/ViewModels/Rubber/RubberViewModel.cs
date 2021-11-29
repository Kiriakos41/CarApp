using CarApp.Models;
using CarApp.Repositories;
using CarApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    public class RubberViewModel : BaseViewModel
    {
        private Rubber _selectedItem;
        public ObservableCollection<Rubber> Items { get; }
        public Command LoadItemsCommand { get; }
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
            Items = new ObservableCollection<Rubber>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Rubber>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            //SortListCommand = new Command(SortList);
        }
        //public void SortList()
        //{
        //    if (!isSort)
        //    {
        //        isSort = true;
        //        var sorted = Items.OrderByDescending(x => x.Price).ToList();
        //        Items.Clear();
        //        Pr = 0;
        //        foreach (var item in sorted)
        //        {
        //            Items.Add(item);
        //            Pr += Convert.ToInt64(item.Price);
        //        }
        //    }
        //    else
        //    {
        //        isSort = false;
        //        var sorted = Items.OrderBy(x => x.Price).ToList();
        //        Items.Clear();
        //        Pr = 0;
        //        foreach (var item in sorted)
        //        {
        //            Items.Add(item);
        //            Pr += Convert.ToInt64(item.Price);
        //        }
        //    }
        //}

        async Task ExecuteLoadItemsCommand()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                Items.Clear();
                var servs = await unitOfwork.RubberTables.Get<Service>();
                foreach (var srv in servs)
                {
                    Items.Add(srv);
                    Pr += Convert.ToInt64(srv.Price);
                }

                //SortList();
                IsBusy = false;
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

            //await Shell.Current.GoToAsync($"{nameof(ServiceDetailPage)}?{nameof(ServiceDetailViewModel.ItemId)}={item.Id}");
        }
    }
}