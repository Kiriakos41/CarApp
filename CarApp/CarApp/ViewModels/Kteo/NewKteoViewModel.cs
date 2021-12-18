using CarApp.Models;
using CarApp.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    public class NewKteoViewModel : BaseViewModel
    {
        private DateTime _date = DateTime.Now;
        private decimal price;
        private string desc;
        private DateTime _next = DateTime.Now.AddYears(2);
        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public string KteoDetail
        {
            get => desc;
            set => SetProperty(ref desc, value);
        }
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }
        public DateTime Next
        {
            get => _next;
            set => SetProperty(ref _next, value);
        }
        public NewKteoViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
        }
        public Command AddEntryCommand { get; set; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        private async void OnCancel()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void OnSave()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                Kteo newItem = new Kteo()
                {
                    Price = Price,
                    KteoDetail = KteoDetail,
                    Date = Date,
                    NextKteo = Next
                };
                await unitOfwork.KteoTables.Insert(newItem);
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
