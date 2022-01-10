using CarApp.Models;
using CarApp.Repositories;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private int itemId;
        private long gas;
        private string gasStation;
        private decimal price;
        private DateTime date;
        public string GasStation
        {
            get => gasStation;
            set => SetProperty(ref gasStation, value);
        }
        public ObservableCollection<AboutCar> car { get; set; } = new ObservableCollection<AboutCar>();
        public int Id { get; set; }
        public long Gas
        {
            get => gas;
            set => SetProperty(ref gas, value);
        }
        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }
        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public Command DeleteCommand { get; set; }
        public Command UpdateCommand { get; set; }

        public ItemDetailViewModel()
        {
            DeleteCommand = new Command(DeleteItem);
            UpdateCommand = new Command(UpdateItem);
        }
        public async void DeleteItem()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var item = await unitOfwork.AboutCars.Get(ItemId);
                await unitOfwork.AboutCars.Delete(item);
                await Shell.Current.GoToAsync("..");
            }

        }
        public async void UpdateItem()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                AboutCar item = await unitOfwork.AboutCars.Get(ItemId);
                item.Id = Id;
                item.Gas = Gas;
                item.Price = Price;
                item.GasName = GasStation;
                await unitOfwork.AboutCars.Update(item);
                await Shell.Current.GoToAsync("..");
            }

        }

        public async void LoadItemId(int itemId)
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var item = await unitOfwork.AboutCars.Get(itemId);
                Id = item.Id;
                Gas = item.Gas;
                Price = item.Price;
                Date = item.Date;
            }
        }
    }
}
