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
        public decimal KilMax
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
        private decimal kilmax;
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
                KilMax = 0;
                var getMax = await unitOfwork.DistanceTable.Get<Distance>();
                if (getMax.Count != 0)
                    KilMax = getMax.Max(x => x.CarDistance);
                Items.Clear();
                Gs = 0;
                Pr = 0;
                WeekPrice = 0;
                MonthPrice = 0;
                ThreeMonthPrice = 0;
                try
                {
                    // List 0
                    var items = await unitOfwork.AboutCars.Get<AboutCar>();
                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            Items.Add(item);
                            Pr += Convert.ToInt64(item.Price);
                            Gs += Convert.ToInt64(item.Gas);
                        }
                        WeekPrice += items.Where(x => x.Date >= DateTime.Now.AddDays(-7) && x.Date <= DateTime.Now).Sum(x => x.Price);
                        MonthPrice += items.Where(x => x.Date >= DateTime.Now.AddDays(-30) && x.Date <= DateTime.Now).Sum(x => x.Price);
                        ThreeMonthPrice += items.Where(x => x.Date >= DateTime.Now.AddDays(-90) && x.Date <= DateTime.Now).Sum(x => x.Price);
                    }
                    //List 1
                    var items2 = await unitOfwork.Damages.Get<Damage>();
                    if (items2 != null)
                    {
                        foreach (var item in items2)
                        {
                            Pr += Convert.ToInt64(item.Price);
                        }
                        WeekPrice += items2.Where(x => x.Date >= DateTime.Now.AddDays(-7) && x.Date <= DateTime.Now).Sum(x => x.Price);
                        MonthPrice += items2.Where(x => x.Date >= DateTime.Now.AddDays(-30) && x.Date <= DateTime.Now).Sum(x => x.Price);
                        ThreeMonthPrice += items2.Where(x => x.Date >= DateTime.Now.AddDays(-90) && x.Date <= DateTime.Now).Sum(x => x.Price);
                    }
                    // List 2
                    var items3 = await unitOfwork.RubberTables.Get<Rubber>();
                    if (items3 != null)
                    {
                        foreach (var item in items3)
                        {
                            Pr += Convert.ToInt64(item.Price);
                        }
                        WeekPrice += items3.Where(x => x.Date >= DateTime.Now.AddDays(-7) && x.Date <= DateTime.Now).Sum(x => x.Price);
                        MonthPrice = items3.Where(x => x.Date >= DateTime.Now.AddDays(-30) && x.Date <= DateTime.Now).Sum(x => x.Price);
                        ThreeMonthPrice = items3.Where(x => x.Date >= DateTime.Now.AddDays(-90) && x.Date <= DateTime.Now).Sum(x => x.Price);
                    }
                    //List 3
                    var items4 = await unitOfwork.ServiceTables.Get<Service>();
                    if (items4 != null)
                    {
                        foreach (var item in items4)
                        {
                            Pr += Convert.ToInt64(item.Price);
                        }
                        WeekPrice += items4.Where(x => x.Date >= DateTime.Now.AddDays(-7) && x.Date <= DateTime.Now).Sum(x => x.Price);
                        MonthPrice += items4.Where(x => x.Date >= DateTime.Now.AddDays(-30) && x.Date <= DateTime.Now).Sum(x => x.Price);
                        ThreeMonthPrice += items4.Where(x => x.Date >= DateTime.Now.AddDays(-90) && x.Date <= DateTime.Now).Sum(x => x.Price);
                    }
                    //List 4
                    var items5 = await unitOfwork.KteoTables.Get<Rubber>();
                    if (items5 != null)
                    {
                        foreach (var item in items5)
                        {
                            Pr += Convert.ToInt64(item.Price);
                        }
                        WeekPrice += items5.Where(x => x.Date >= DateTime.Now.AddDays(-7) && x.Date <= DateTime.Now).Sum(x => x.Price);
                        MonthPrice += items5.Where(x => x.Date >= DateTime.Now.AddDays(-30) && x.Date <= DateTime.Now).Sum(x => x.Price);
                        ThreeMonthPrice = items5.Where(x => x.Date >= DateTime.Now.AddDays(-90) && x.Date <= DateTime.Now).Sum(x => x.Price);
                        //
                    }
                    //

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
                Pr += Convert.ToInt64(item.Price);
                Gs += Convert.ToInt64(item.Gas);
            }
        }
    }
}