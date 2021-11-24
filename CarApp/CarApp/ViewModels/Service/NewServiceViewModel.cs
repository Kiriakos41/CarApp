using CarApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using CarApp.Repositories;
using CarApp.Helpers;
using static CarApp.Helpers.Extensions;

namespace CarApp.ViewModels
{
    public class NewServiceViewModel : BaseViewModel
    {
        private string changes;
        private string price;
        private IList<object> _selectedServices;
        private List<BindingEnum> services;

        public IList<object> SelectedServices
        {
            get => _selectedServices;
            set => SetProperty(ref _selectedServices, value);
        }

        public List<BindingEnum> Services { 
            get => services;
            set => SetProperty(ref services, value);
        }


        public NewServiceViewModel()
        {
            Services = new BindingEnum().BindEnumToSelectListItem<ServiceEnum>();
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
        public string Price
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
                var sr = SelectedServices.ToList();
                var newSr = sr.Cast<BindingEnum>().ToList();
                Service newItem = new Service()
                {
                    Changes = string.Join(",", newSr.Select(x => x.Text)),
                    Price = Price,
                    NumberOfChanges = sr.Count,
                };
                await unitOfwork.ServiceTables.Insert(newItem);

                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
