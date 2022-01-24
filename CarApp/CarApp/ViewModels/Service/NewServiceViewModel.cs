using CarApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using CarApp.Repositories;

namespace CarApp.ViewModels
{
    public class NewServiceViewModel : BaseViewModel
    {
        private string changes;
        private decimal price;
        private DateTime date = DateTime.Now;
        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public NewServiceViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
        public string Changes
        {
            get => changes;
            set => SetProperty(ref changes, value);
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
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        private async void OnSave()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                Service newItem = new Service()
                {
                    Changes = Changes,
                    Price = Price,
                    Date = Date
                };
                await unitOfwork.ServiceTables.Insert(newItem);

                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
