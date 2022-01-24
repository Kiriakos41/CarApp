using CarApp.Models;
using CarApp.Repositories;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class KteoDetailViewModel : BaseViewModel
    {
        public KteoDetailViewModel()
        {
            UpdateCommand = new Command(UpdateItem);
            DeleteCommand = new Command(DeleteItem);
        }
        private string kteoDetail;
        private DateTime date;
        private DateTime next;
        private int itemId;
        private decimal price;
        public string KteoDetail
        {
            get => kteoDetail;
            set => SetProperty(ref kteoDetail, value);
        }
        public DateTime Next
        {
            get => next;
            set => SetProperty(ref next, value);
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
        public Command UpdateCommand { get; set; }

        public async void LoadItemId(int itemId)
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var item = await unitOfwork.KteoTables.Get(itemId);
                Id = item.Id;
                Price = item.Price;
                Date = item.Date;
                KteoDetail = item.KteoDetail;
                Next = item.NextKteo;
            }
        }
        public async void DeleteItem()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                Kteo item = await unitOfwork.KteoTables.Get(itemId);
                await unitOfwork.KteoTables.Delete(item);
                await Shell.Current.GoToAsync("..");
            }
        }
        public async void UpdateItem()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var item = await unitOfwork.KteoTables.Get(itemId);
                item.Id = Id;
                item.Price = Price;
                item.KteoDetail = KteoDetail;
                item.NextKteo = Next;
                item.Date = Date;
                await unitOfwork.KteoTables.Update(item);
                await Shell.Current.GoToAsync("..");
            }
            var notification = new NotificationRequest
            {
                BadgeNumber = 1,
                NotificationId = Id,
                Description = "ΤΟ ΕΠΟΜΕΝΟ ΣΑΣ ΚΤΕΟ " + Next,
                Title = "ΕΝΗΜΕΡΩΣΗ ΚΤΕΟ",
                Schedule = {
                        NotifyTime = Next
                    }
            };
            await NotificationCenter.Current.Show(notification);
        }
    }
}