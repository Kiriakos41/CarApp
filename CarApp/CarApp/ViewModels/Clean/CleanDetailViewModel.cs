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
        private string price;
        public List<string> Rating { get; set; }
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
        public string Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public Command DeleteCommand { get; set; }
        public Command UpdateCommand { get; set; }
        public Command CancelCommand { get; set; }

        public CleanDetailViewModel()
        {
            Rating = new List<string> { "Απλή", "Μέτρια", "Άριστη" };
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
            }
        }
    }
}
