using CarApp.Models;
using CarApp.Repositories;
using CarApp.Views;
using MvvmHelpers;
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
        public ObservableCollection<Damage> Items { get; }
        public ObservableCollection<Grouping<string, Damage>> SortedItem { get; set; } = new ObservableCollection<Grouping<string, Damage>>();
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Damage> ItemTapped { get; }
        public Command SortListCommand { get; }

        public DamageViewModel()
        {
            Items = new ObservableCollection<Damage>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Damage>(OnItemSelected);

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
        //        Price = 0;
        //        foreach (var item in sorted)
        //        {
        //            Items.Add(item);
        //            Price += Convert.ToInt64(item.Price);
        //        }
        //    }
        //    else
        //    {
        //        isSort = false;
        //        var sorted = Items.OrderBy(x => x.Price).ToList();
        //        Items.Clear();
        //        Price = 0;
        //        foreach (var item in sorted)
        //        {
        //            Items.Add(item);
        //            Price += Convert.ToInt64(item.Price);
        //        }
        //    }
        //}

        async Task ExecuteLoadItemsCommand()
        {
            Price = 0;
            Items.Clear();
            SortedItem.Clear();
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                try
                {
                    var damages = await unitOfwork.Damages.Get<Damage>();
                    foreach (var dmg in damages)
                    {
                        Items.Add(dmg);
                        //Price += Convert.ToInt64(refil.Price);
                    }
                    ///////////
                    // SortList();

                    var groupItem = from dmg in Items
                                    orderby dmg.Date
                                    group dmg by dmg.Date.ToString(String.Format("{0:D}", dmg.Date)) into dmgGroup
                                    select new Grouping<string, Damage>(dmgGroup.Key, dmgGroup);

                    foreach (var item in groupItem)
                    {
                        SortedItem.Add(item);
                    }

                    IsBusy = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
            ExecuteLoadItemsCommand();
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
