using CarApp.Models;
using CarApp.Services;
using CarApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace CarApp.ViewModels
{

    public class MyCarViewModel : BaseViewModel
    {
        private string id;
        private DateTime dateTo;
        private DateTime dateFrom;

        public DateTime DateTo
        {
            get => dateTo;
            set => SetProperty(ref dateTo, value);
        }
        public DateTime DateFrom
        {
            get => dateFrom;
            set => SetProperty(ref dateFrom, value);
        }
        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        public long Pr
        {
            get => pr;
            set => SetProperty(ref pr, value);
        }
        public long KilMax
        {
            get => kilmax;
            set => SetProperty(ref kilmax, value);
        }
        public long Gs
        {
            get => gs;
            set => SetProperty(ref gs, value);
        }



        private long pr;
        private long kilmax;
        private long gs;
        public ObservableCollection<AboutCar> Items { get; set; }
        public Command SortCommand { get; set; }

        public MyCarViewModel()
        {
            SortCommand = new Command(SortByDate);
            Items = new ObservableCollection<AboutCar>();
            DateTo = DateTime.Now;
        }

        public async Task OnAppearAsync()
        {
            try
            {
                Gs = 0;
                Pr = 0;
                KilMax = 0;
                var items = await DataStore.GetItemsAsync(true);
                var items1 = await CleanData.GetItemsAsync(true);
                var items2 = await ServiceData.GetItemsAsync(true);
                KilMax = items.Select(x => x.Kilometer).Max();
                foreach (var item in items)
                {
                    Items.Add(item);
                    KilMax += item.Kilometer;
                    Pr += Convert.ToInt64(item.Price);
                    Gs += Convert.ToInt64(item.Gas);
                }
                foreach (var item in items1)
                {
                    Pr += Convert.ToInt64(item.Price);
                }
                foreach (var item in items2)
                {
                    Pr += Convert.ToInt64(item.Price);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void SortByDate()
        {
            Gs = 0;
            Pr = 0;
            KilMax = 0;
            var newItems = Items.Where(x => x.Date >= DateFrom && x.Date <= DateTo).ToList();
            foreach (var item in newItems)
            {
                KilMax += item.Kilometer;
                Pr += Convert.ToInt64(item.Price);
                Gs += Convert.ToInt64(item.Gas);
            }
        }
        private async void OnSave()
        {

            await Shell.Current.GoToAsync(nameof(ItemsPage));
        }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync(nameof(ItemsPage));
        }
    }
}