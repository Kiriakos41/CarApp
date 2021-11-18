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
        private bool show;
        public bool Show
        {
            get => show;
            set => SetProperty(ref show, value);
        }
        private bool showsecond = true;
        public bool ShowSecond
        {
            get => showsecond;
            set => SetProperty(ref showsecond, value);
        }

        private IList<object> _selectedServices;
        public IList<object> SelectedServices
        {
            get => _selectedServices;
            set => SetProperty(ref _selectedServices, value);
        }
        public ObservableCollection<string> services { get; set; } = new ObservableCollection<string>()
        {
            "Λάδια","Φίλτρο Λαδιού","Φίλτρο","Φίλτρο Αέρος","Μπουζί","Φίλτρο καμπίνας","Βαλβολίνες","Φρένα Εμπρός","Φρένα Πίσω","Ιμάντας Χρονισμού","Ιμάντας Δυναμό","Υγρά Φρένων"
        };


        public string Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }


        public ObservableCollection<string> SelectedItems { get; set; }

        public Command DeleteCommand { get; set; }
        public Command UpdateCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command PopUp { get; set; }

        public ServiceDetailViewModel()
        {
            DeleteCommand = new Command(DeleteItem);
            UpdateCommand = new Command(UpdateItem);
            CancelCommand = new Command(OnCancel);
            PopUp = new Command(ChangeList);
            SelectedItems = new ObservableCollection<string>();
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
            var item = await ServiceData.GetItemAsync(itemId);
            item.Id = Id;
            item.Price = Price;
            item.Changes = SelectedServices.Count.ToString();
            item.Ser = SelectedServices.Select(x => x.ToString()).ToList();
            await ServiceData.UpdateItemAsync(item);
            LoadItemId(item.Id);
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                SelectedItems.Clear();
                var item = await ServiceData.GetItemAsync(itemId);
                Id = item.Id;
                Price = item.Price;
                var list = item.Ser;
                foreach (var sr in list)
                {
                    SelectedItems.Add(sr);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public void ChangeList()
        {
            var sr = SelectedServices.ToList();
            if (Show)
            {
                Show = false;
                ShowSecond = true;
                if(sr.Count > 0)
                {
                    UpdateItem();
                }
            }
            else
            {
                Show = true;
                ShowSecond = false;
            }
        }
    }
}