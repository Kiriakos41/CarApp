using CarApp.Models;
using CarApp.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class RubberDetailViewModel : BaseViewModel
    {
        private int itemId;
        private decimal price;
        private string commend;
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
        public string Commend
        {
            get => commend;
            set => SetProperty(ref commend, value);
        }
        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public Command DeleteCommand { get; set; }
        public Command UpdateCommand { get; set; }
        public Command CancelCommand { get; set; }

        public RubberDetailViewModel()
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
                Rubber item = await unitOfwork.RubberTables.Get(itemId);
                await unitOfwork.RubberTables.Delete(item);
                await Shell.Current.GoToAsync("..");
            }
        }
        public async void UpdateItem()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var item = await unitOfwork.RubberTables.Get(itemId);
                item.Id = Id;
                item.Price = Price;
                item.Commend = Commend;
                item.Date = Date;
                await unitOfwork.RubberTables.Update(item);
                await Shell.Current.GoToAsync("..");
            }
        }

        public async void LoadItemId(int itemId)
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var item = await unitOfwork.RubberTables.Get(itemId);
                Id = item.Id;
                Price = item.Price;
                Commend = item.Commend;
                Date = item.Date;
            }
        }
    }
}
