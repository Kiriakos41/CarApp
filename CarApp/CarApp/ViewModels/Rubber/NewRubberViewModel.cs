using CarApp.Models;
using CarApp.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    public class NewRubberViewModel : BaseViewModel
    {
        private DateTime date = DateTime.Now;
        private decimal price;
        private string commend;
        public string Commend
        {
            get => commend;
            set => SetProperty(ref commend, value);
        }
        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public NewRubberViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
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
                Rubber newItem = new Rubber()
                {
                    Date = Date,
                    Price = Price,
                    Commend = Commend
                };

                await unitOfwork.RubberTables.Insert(newItem);

                await Shell.Current.GoToAsync("..");
            }

        }
    }
}
