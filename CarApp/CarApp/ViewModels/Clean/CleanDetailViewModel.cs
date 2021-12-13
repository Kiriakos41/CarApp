using CarApp.Models;
using CarApp.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class CleanDetailViewModel : BaseViewModel
    {
        private int itemId;
        private string quality;
        private decimal price;
        private DateTime date;
        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public List<string> Rating { get; set; } = new List<string> { "Απλή", "Μέτρια", "Άριστη" };
        public int Id { get; set; }
        public string Quality
        {
            get => quality;
            set => SetProperty(ref quality, value);
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
        public Command CancelCommand { get; set; }

        public CleanDetailViewModel()
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
                Clean item = await unitOfwork.Cleans.Get(itemId);
                await unitOfwork.Cleans.Delete(item);
                await Shell.Current.GoToAsync("..");
            }
        }
        public async void UpdateItem()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var item = await unitOfwork.Cleans.Get(itemId);
                item.Id = Id;
                item.Quality = Quality;
                item.Price = Price;
                item.Date = Date;
                await unitOfwork.Cleans.Update(item);
                await Shell.Current.GoToAsync("..");
            }
        }

        public async void LoadItemId(int itemId)
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var item = await unitOfwork.Cleans.Get(itemId);
                Id = item.Id;
                Quality = item.Quality;
                Price = item.Price;
                Date = item.Date;
            }
        }
    }
}
