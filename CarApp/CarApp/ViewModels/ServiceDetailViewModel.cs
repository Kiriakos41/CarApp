using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ServiceDetailViewModel : BaseViewModel
    {
        private string itemId;
        public string Id { get; set; }
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
        private string price;
        public string Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public List<string> Items { get; set; }

        public Command DeleteCommand { get; set; }
        public Command UpdateCommand { get; set; }
        public Command CancelCommand { get; set; }

        public ServiceDetailViewModel()
        {
            DeleteCommand = new Command(DeleteItem);
            UpdateCommand = new Command(UpdateItem);
            CancelCommand = new Command(OnCancel);
            Items = new List<string>();
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
            await CleanData.UpdateItemAsync(item);
            await Shell.Current.GoToAsync("..");
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await ServiceData.GetItemAsync(itemId);
                Id = item.Id;
                Price = item.Price;
                var list = item.Ser;
                foreach (var sr in list)
                {
                    Items.Add(sr);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}