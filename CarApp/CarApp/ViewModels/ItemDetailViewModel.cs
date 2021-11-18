using CarApp.Models;
using CarApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string gas;
        private string khm;
        private string price;
        private DateTime date;
        public ObservableCollection<AboutCar> car { get; set; } = new ObservableCollection<AboutCar>();
        public string Id { get; set; }
        public string Gas
        {
            get => gas;
            set => SetProperty(ref gas, value);
        }
        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public string Khm
        {
            get => khm;
            set => SetProperty(ref khm, value);
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

        public ItemDetailViewModel()
        {
            DeleteCommand = new Command(DeleteItem);
            UpdateCommand = new Command(UpdateItem);
        }
        public async void DeleteItem()
        {
            var item = await DataStore.GetItemAsync(itemId);
            await DataStore.DeleteItemAsync(ItemId);
            await Shell.Current.GoToAsync("..");
        }
        public async void UpdateItem()
        {
            var item = await DataStore.GetItemAsync(itemId);
            item.Id = Id;
            item.Gas = Gas;
            item.Kilometer = Khm;
            item.Price = Price;
            await DataStore.UpdateItemAsync(item);
            await Shell.Current.GoToAsync("..");
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Gas = item.Gas;
                Khm = item.Kilometer;
                Price = item.Price;
                Date = item.Date;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
