using CarApp.Models;
using CarApp.Repositories;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class DistanceDetailViewModel : BaseViewModel
    {
        private int itemId;
        private decimal carDistance;
        private DateTime date;
        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public int Id { get; set; }
        public decimal CarDistance
        {
            get => carDistance;
            set => SetProperty(ref carDistance, value);
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
        public Command DeleteCommand { get; set; }
        public Command UpdateCommand { get; set; }
        public Command CancelCommand { get; set; }

        public DistanceDetailViewModel()
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
                Distance item = await unitOfwork.DistanceTable.Get(itemId);
                await unitOfwork.DistanceTable.Delete(item);
                await Shell.Current.GoToAsync("..");
            }
        }
        public async void UpdateItem()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var item = await unitOfwork.DistanceTable.Get(itemId);
                item.Id = Id;
                item.CarDistance = CarDistance;
                item.Date = Date;
                await unitOfwork.DistanceTable.Update(item);
                await Shell.Current.GoToAsync("..");
            }
        }

        public async void LoadItemId(int itemId)
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var item = await unitOfwork.DistanceTable.Get(itemId);
                Id = item.Id;
                CarDistance = item.CarDistance;
                Date = item.Date;
            }
        }
    }
}
