using CarApp.Models;
using CarApp.Repositories;
using System;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    public class NewProtectionViewModel : BaseViewModel
    {
        private DateTime _date = DateTime.Now;
        private decimal price;
        private string protName;
        public decimal ProtectionPrice
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public string ProtectionName
        {
            get => protName;
            set => SetProperty(ref protName, value);
        }
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }
        public NewProtectionViewModel()
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
                Protection newItem = new Protection()
                {
                    ProtectionPrice = ProtectionPrice,
                    ProtectionName = ProtectionName,
                    ProtectionDate = Date,
                };
                await unitOfwork.ProtectionTables.Insert(newItem);
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
