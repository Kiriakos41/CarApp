using CarApp.Models;
using CarApp.Repositories;
using System;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class DamageDetailViewModel : BaseViewModel
    {
        public DamageDetailViewModel()
        {
            DeleteCommand = new Command(DeleteItem);
        }
        private string description;
        private DateTime date;
        private int itemId;
        private decimal price;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
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
        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public Command DeleteCommand { get; set; }

        public async void LoadItemId(int itemId)
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var item = await unitOfwork.Damages.Get(itemId);
                Id = item.Id;
                Price = item.Price;
                Date = item.Date;
                Description = item.Description;
            }
        }
        public async void DeleteItem()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                Damage item = await unitOfwork.Damages.Get(itemId);
                await unitOfwork.Damages.Delete(item);
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
