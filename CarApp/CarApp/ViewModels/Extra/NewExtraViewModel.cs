using CarApp.Models;
using CarApp.Repositories;
using System;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    public class NewExtraViewModel : BaseViewModel
    {
        private string desc;
        private decimal price;
        private DateTime date = DateTime.Now;

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public NewExtraViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public string Description
        {
            get => desc;
            set => SetProperty(ref desc, value);
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
                Extra newItem = new Extra()
                {
                    Description = desc,
                    Price = Price,
                    Date = Date,
                };

                await unitOfwork.ExtraTables.Insert(newItem);

                await Shell.Current.GoToAsync("..");
            }

        }
    }
}
