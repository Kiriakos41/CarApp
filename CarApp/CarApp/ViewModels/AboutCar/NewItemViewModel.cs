using CarApp.Models;
using CarApp.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private long gas;
        private long khm;
        private string price;
        private DateTime date;

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public NewItemViewModel()
        {
            Date = DateTime.Now;
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public long Gas
        {
            get => gas;
            set => SetProperty(ref gas, value);
        }
        public string Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public long Khm
        {
            get => khm;
            set => SetProperty(ref khm, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                AboutCar newItem = new AboutCar()
                {
                    Gas = Gas,
                    Kilometer = Khm,
                    Price = Price,
                    Date = Date,
                };

                await unitOfwork.AboutCars.Insert(newItem);

                await Shell.Current.GoToAsync("..");
            }

        }
    }
}
