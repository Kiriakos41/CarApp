using CarApp.Models;
using CarApp.Repositories;
using System;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private long gas;
        private decimal price;
        private string gasStation;
        private DateTime date = DateTime.Now;

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public string GasStation
        {
            get => gasStation;
            set => SetProperty<string>(ref gasStation, value);
        }
        public NewItemViewModel()
        {
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
        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                AboutCar newItem = new AboutCar()
                {
                    Gas = Gas,
                    Price = Price,
                    Date = Date,
                    GasName = GasStation
                };

                await unitOfwork.AboutCars.Insert(newItem);

                await Shell.Current.GoToAsync("..");
            }

        }
    }
}
