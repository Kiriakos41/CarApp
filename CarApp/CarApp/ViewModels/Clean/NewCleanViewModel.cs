using CarApp.Models;
using CarApp.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    public class NewCleanViewModel : BaseViewModel
    {
        private string quality;
        private decimal price;
        private DateTime date = DateTime.Now;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public List<string> Rating { get; set; } = new List<string>()
        {
            "Απλή","Μέτρια","Τέλεια"
        };
        public NewCleanViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
        }
        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public string Quality
        {
            get => quality;
            set => SetProperty(ref quality, value);
        }
        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        private async void OnSave()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                Clean newItem = new Clean()
                {
                    Quality = Quality,
                    Price = Price,
                    Date = Date
                };
                await unitOfwork.Cleans.Insert(newItem);

                // This will pop the current page off the navigation stack
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
