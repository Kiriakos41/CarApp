using CarApp.Models;
using CarApp.Repositories;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
namespace CarApp.ViewModels
{
    public class NewDistanceViewModel : BaseViewModel
    {
        private decimal carDistance;
        private DateTime date = DateTime.Now;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public NewDistanceViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
        }
        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public decimal CarDistance
        {
            get => carDistance;
            set => SetProperty(ref carDistance, value);
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
                Distance newItem = new Distance()
                {
                    CarDistance = CarDistance,
                    Date = Date
                };
                await unitOfwork.DistanceTable.Insert(newItem);

                // This will pop the current page off the navigation stack
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
