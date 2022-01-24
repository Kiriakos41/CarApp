using CarApp.Models;
using CarApp.Repositories;
using CarApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ServiceDetailViewModel : BaseViewModel
    {
        private int itemId;
        private string change;
        private DateTime date;
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
        private decimal price;
        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public string Changes
        {
            get => change;
            set => SetProperty(ref change, value);
        }
        public Command DeleteCommand { get; set; }
        public Command UpdateCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command PopUp { get; set; }

        public ServiceDetailViewModel()
        {
            DeleteCommand = new Command(DeleteItem);
            UpdateCommand = new Command(UpdateItem);
            CancelCommand = new Command(OnCancel);
        }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        public async void DeleteItem()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                Service item = await unitOfwork.ServiceTables.Get(ItemId);
                await unitOfwork.ServiceTables.Delete(item);
                await Shell.Current.GoToAsync("..");
            }

        }
        public async void UpdateItem()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var item = await unitOfwork.ServiceTables.Get(itemId);
                item.Id = Id;
                item.Price = Price;
                item.Changes = Changes;
                item.Date = Date;
                await unitOfwork.ServiceTables.Update(item);
                await Shell.Current.GoToAsync("..");
            }
        }

        public async void LoadItemId(int itemId)
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var item = await unitOfwork.ServiceTables.Get(itemId);
                Id = item.Id;
                Price = item.Price;
                Changes = item.Changes;
                Date = item.Date;
            }
        }
    }
}