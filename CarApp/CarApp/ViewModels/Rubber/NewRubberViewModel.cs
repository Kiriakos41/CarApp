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
        private long gas;
        private decimal price;
        private string commend;
        public string Commend
        {
            get => commend;
            set => SetProperty(ref commend, value);
        }
        private DateTime date;

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public NewRubberViewModel()
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
