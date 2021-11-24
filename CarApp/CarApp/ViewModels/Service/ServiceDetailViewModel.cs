using CarApp.Helpers;
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
using static CarApp.Helpers.Extensions;

namespace CarApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ServiceDetailViewModel : BaseViewModel
    {
        private int itemId;
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
        private string price;
        private List<BindingEnum> services;
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
        public List<BindingEnum> Services
        {
            get => services;
            set => SetProperty(ref services, value);
        }
        public string Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        private string[] selitem;
        public string[] SelectedItems 
        {
            get => selitem; 
            set => SetProperty(ref selitem, value);
        }


        public void FillServices()
        {
            Services = new BindingEnum().BindEnumToSelectListItem<ServiceEnum>();
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
            PopUp = new Command(ChangeList);
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
            var sr = SelectedServices.ToList();
            var newSr = sr.Cast<BindingEnum>().ToList();
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var item = await unitOfwork.ServiceTables.Get(itemId);
                item.Id = Id;
                item.Price = Price;
                item.Changes = null;
                item.Changes = string.Join(",", newSr.Select(x => x.Text));
                item.NumberOfChanges = sr.Count;
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
                SelectedItems = item.Changes.Split(',');
            }
        }

        public void ChangeList()
        {
            var sr = SelectedServices.ToList();
            if (Show)
            {
                Show = false;
                ShowSecond = true;
                if (sr.Count > 0)
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