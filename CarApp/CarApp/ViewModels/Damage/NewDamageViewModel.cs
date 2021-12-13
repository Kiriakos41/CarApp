using CarApp.Models;
using CarApp.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    public class NewDamageViewModel : BaseViewModel
    {
        private DateTime _date = DateTime.Now;
        private decimal price;
        private string desc;
        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public string Description
        {
            get => desc;
            set => SetProperty(ref desc, value);
        }
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }
        public NewDamageViewModel()
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
                Damage newItem = new Damage()
                {
                    Price = Price,
                    Description = Description,
                    Date = Date
                };
                await unitOfwork.Damages.Insert(newItem);
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
