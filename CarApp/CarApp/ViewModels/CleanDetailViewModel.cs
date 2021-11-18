using CarApp.Models;
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
        private string itemId;
        private string quality;
        private string price;
        public List<string> Rating { get; set; }
        public string Id { get; set; }
        public string Quality
        {
            get => quality;
            set => SetProperty(ref quality, value);
        }
        public string ItemId
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
            var item = await CleanData.GetItemAsync(itemId);
            await CleanData.DeleteItemAsync(ItemId);
            await Shell.Current.GoToAsync("..");
        }
        public async void UpdateItem()
        {
            var item = await CleanData.GetItemAsync(itemId);
            item.Id = Id;
            item.Quality = Quality;
            item.Price = Price;
            await CleanData.UpdateItemAsync(item);
            await Shell.Current.GoToAsync("..");
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await CleanData.GetItemAsync(itemId);
                Id = item.Id;
                Quality = item.Quality;
                Price = item.Price;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
