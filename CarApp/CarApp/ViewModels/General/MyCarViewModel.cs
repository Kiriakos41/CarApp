﻿using CarApp.Models;
using CarApp.Repositories;
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
        public Command ChartsCommand { get; set; }
        public MyCarViewModel()
        {
            SortCommand = new Command(SortByDate);
            ChartsCommand = new Command(GoToCharts);
            Items = new ObservableCollection<AboutCar>();
            DateTo = DateTime.Now;
        }
        public void GoToCharts()
        {
            Shell.Current.CurrentItem.CurrentItem.Items.Add(new ChartsPage());
            Shell.Current.CurrentItem.CurrentItem.Items.RemoveAt(0);
        }
        public async Task ExecuteLoadItemsCommand()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                Gs = 0;
                Pr = 0;
                KilMax = 0;
                try
                {
                    var items = await unitOfwork.AboutCars.Get<AboutCar>();
                    foreach (var item in items)
                    {
                        Items.Add(item);
                        KilMax += item.Kilometer;
                        Pr += Convert.ToInt64(item.Price);
                        Gs += Convert.ToInt64(item.Gas);
                    }
                    var items1 = await unitOfwork.Cleans.Get<Clean>();
                    foreach (var item in items1)
                    {
                        Pr += Convert.ToInt64(item.Price);
                    }
                    var items2 = await unitOfwork.ServiceTables.Get<Clean>();
                    foreach (var item in items2)
                    {
                        Pr += Convert.ToInt64(item.Price);
                    }
                    var items3 = await unitOfwork.Damages.Get<Damage>();
                    foreach (var item in items3)
                    {
                        Pr += Convert.ToInt64(item.Price);
                    }
                    var items4 = await unitOfwork.RubberTables.Get<Rubber>();
                    foreach (var item in items4)
                    {
                        Pr += Convert.ToInt64(item.Price);
                    }
                    KilMax = items.Select(x => x.Kilometer).Max();
                    IsBusy = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
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