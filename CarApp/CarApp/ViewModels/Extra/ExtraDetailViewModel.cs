using CarApp.Models;
using CarApp.Repositories;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;


namespace CarApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ExtraDetailViewModel : BaseViewModel
    {
        private int itemId;
        private decimal price;
        private string desc;
        private DateTime date;
        public ObservableCollection<Extra> car { get; set; } = new ObservableCollection<Extra>();
        public int Id { get; set; }
        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public string Description
        {
            get => desc;
            set => SetProperty<string>(ref desc, value);
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

        public ExtraDetailViewModel()
        {
            DeleteCommand = new Command(DeleteItem);
            UpdateCommand = new Command(UpdateItem);
        }
        public async void DeleteItem()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var item = await unitOfwork.ExtraTables.Get(ItemId);
                await unitOfwork.ExtraTables.Delete(item);
                await Shell.Current.GoToAsync("..");
            }

        }
        public async void UpdateItem()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                Extra item = await unitOfwork.ExtraTables.Get(ItemId);
                item.Id = Id;
                item.Price = Price;
                item.Description = Description;
                item.Date = Date;
                await unitOfwork.ExtraTables.Update(item);
                await Shell.Current.GoToAsync("..");
            }

        }

        public async void LoadItemId(int itemId)
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var item = await unitOfwork.ExtraTables.Get(itemId);
                Id = item.Id;
                Price = item.Price;
                Date = item.Date;
                Description = item.Description;
            }
        }
    }
}
