using CarApp.Models;
using CarApp.Repositories;
using System;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ProtectionDetailViewModel : BaseViewModel
    {
        public ProtectionDetailViewModel()
        {
            DeleteCommand = new Command(DeleteItem);
            UpdateCommand = new Command(UpdateItem);
        }
        private string protName;
        private DateTime date;
        private int itemId;
        private decimal price;
        public string ProtectionName
        {
            get => protName;
            set => SetProperty(ref protName, value);
        }
        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public int Id { get; set; }
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
        public decimal ProtectionPrice
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public Command DeleteCommand { get; set; }
        public Command UpdateCommand { get; set; }

        public async void LoadItemId(int itemId)
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var item = await unitOfwork.ProtectionTables.Get(itemId);
                Id = item.Id;
                ProtectionPrice = item.ProtectionPrice;
                Date = item.ProtectionDate;
                ProtectionName = item.ProtectionName;
            }
        }
        public async void UpdateItem()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                Protection item = await unitOfwork.ProtectionTables.Get(ItemId);
                item.Id = Id;
                item.ProtectionName = ProtectionName;
                item.ProtectionPrice = ProtectionPrice;
                item.ProtectionDate = Date;
                await unitOfwork.ProtectionTables.Update(item);
                await Shell.Current.GoToAsync("..");
            }

        }
        public async void DeleteItem()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                Protection item = await unitOfwork.ProtectionTables.Get(itemId);
                await unitOfwork.ProtectionTables.Delete(item);
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
