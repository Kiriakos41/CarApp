using CarApp.Models;
using CarApp.Repositories;
using CarApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
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
        private decimal monthPrice;
        private decimal weekPrice;
        private decimal threeMonthPrice;
        public decimal ThreeMonthPrice
        {
            get => threeMonthPrice;
            set => SetProperty(ref threeMonthPrice, value);
        }
        public decimal WeekPrice
        {
            get => weekPrice;
            set => SetProperty(ref weekPrice, value);
        }
        public decimal MonthPrice
        {
            get => monthPrice;
            set => SetProperty(ref monthPrice, value);
        }
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

        public async Task ExecuteLoadItemsCommand()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                Items.Clear();
                Gs = 0;
                Pr = 0;
                KilMax = 0;
                WeekPrice = 0;
                MonthPrice = 0;
                ThreeMonthPrice = 0;
                try
                {
                    // List 0
                    var items = await unitOfwork.AboutCars.Get<AboutCar>();
                    foreach (var item in items)
                    {
                        Items.Add(item);
                        KilMax += item.Kilometer;
                        Pr += Convert.ToInt64(item.Price);
                        Gs += Convert.ToInt64(item.Gas);
                    }
                    var smg = items
                    .Where(x => x.Date >= DateTime.Now.AddDays(-7) && x.Date <= DateTime.Now)
                    .Sum(x => x.Price);
                    var smgMon = items
                    .Where(x => x.Date >= DateTime.Now.AddDays(-30) && x.Date <= DateTime.Now)
                    .Sum(x => x.Price);
                    var smg3Mon = items
                    .Where(x => x.Date >= DateTime.Now.AddDays(-90) && x.Date <= DateTime.Now)
                    .Sum(x => x.Price);
                    // List 1
                    var items1 = await unitOfwork.Cleans.Get<Clean>();
                    foreach (var item in items1)
                    {
                        Pr += Convert.ToInt64(item.Price);
                    }
                    var smg1 = items1
                    .Where(x => x.Date >= DateTime.Now.AddDays(-7) && x.Date <= DateTime.Now)
                    .Sum(x => x.Price);
                    var smgMon1 = items1
                    .Where(x => x.Date >= DateTime.Now.AddDays(-30) && x.Date <= DateTime.Now)
                    .Sum(x => x.Price);
                    var smg3Mon1 = items1
                    .Where(x => x.Date >= DateTime.Now.AddDays(-90) && x.Date <= DateTime.Now)
                    .Sum(x => x.Price);
                    //List 2
                    var items2 = await unitOfwork.Damages.Get<Damage>();
                    foreach (var item in items2)
                    {
                        Pr += Convert.ToInt64(item.Price);
                    }
                    var smg2 = items2
                    .Where(x => x.Date >= DateTime.Now.AddDays(-7) && x.Date <= DateTime.Now)
                    .Sum(x => x.Price);
                    var smgMon2 = items2
                    .Where(x => x.Date >= DateTime.Now.AddDays(-30) && x.Date <= DateTime.Now)
                    .Sum(x => x.Price);
                    var smg3Mon2 = items2
                    .Where(x => x.Date >= DateTime.Now.AddDays(-90) && x.Date <= DateTime.Now)
                    .Sum(x => x.Price);
                    // List 4
                    var items3 = await unitOfwork.RubberTables.Get<Rubber>();
                    foreach (var item in items3)
                    {
                        Pr += Convert.ToInt64(item.Price);
                    }
                    var smg3 = items3
                    .Where(x => x.Date >= DateTime.Now.AddDays(-7) && x.Date <= DateTime.Now)
                    .Sum(x => x.Price);
                    var smgMon3 = items3
                    .Where(x => x.Date >= DateTime.Now.AddDays(-30) && x.Date <= DateTime.Now)
                    .Sum(x => x.Price);
                    var smg3Mon3 = items3
                    .Where(x => x.Date >= DateTime.Now.AddDays(-90) && x.Date <= DateTime.Now)
                    .Sum(x => x.Price);
                    //List 5
                    var items4 = await unitOfwork.ServiceTables.Get<Service>();
                    foreach (var item in items4)
                    {
                        Pr += Convert.ToInt64(item.Price);
                    }
                    //List 6
                    var items5 = await unitOfwork.KteoTables.Get<Rubber>();
                    foreach (var item in items5)
                    {
                        Pr += Convert.ToInt64(item.Price);
                    }
                    var smg5 = items5
                    .Where(x => x.Date >= DateTime.Now.AddDays(-7) && x.Date <= DateTime.Now)
                    .Sum(x => x.Price);
                    var smgMon5 = items5
                    .Where(x => x.Date >= DateTime.Now.AddDays(-30) && x.Date <= DateTime.Now)
                    .Sum(x => x.Price);
                    var smg3Mon5 = items5
                    .Where(x => x.Date >= DateTime.Now.AddDays(-90) && x.Date <= DateTime.Now)
                    .Sum(x => x.Price);
                    //
                    var smg4 = items4
                    .Where(x => x.Date >= DateTime.Now.AddDays(-7) && x.Date <= DateTime.Now)
                    .Sum(x => x.Price);
                    var smgMon4 = items4
                    .Where(x => x.Date >= DateTime.Now.AddDays(-30) && x.Date <= DateTime.Now)
                    .Sum(x => x.Price);
                    var smg3Mon4 = items4
                    .Where(x => x.Date >= DateTime.Now.AddDays(-90) && x.Date <= DateTime.Now)
                    .Sum(x => x.Price);
                    //
                    KilMax = items.Select(x => x.Kilometer).Max();
                    WeekPrice = 0;
                    MonthPrice = 0;
                    ThreeMonthPrice = 0;
                    WeekPrice += smg + smg1 + smg2 + smg3 + smg4 + smg5;
                    MonthPrice += smgMon + smgMon2 + smgMon3 + smgMon4 + smgMon5;
                    ThreeMonthPrice += smg3Mon + smg3Mon1 + smg3Mon2 + smg3Mon3 + smg3Mon4 + smg3Mon5;
                    IsBusy = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public async void OnAppear()
        {
            await ExecuteLoadItemsCommand();
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

    }
}