using CarApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace CarApp.ViewModels
{
    public class NewServiceViewModel : BaseViewModel
    {
        private string changes;
        private string price;
        private IList<object> _selectedServices;
        public IList<object> SelectedServices
        {
            get => _selectedServices;
            set => SetProperty(ref _selectedServices, value);
        }
        public List<string> services { get; set; } = new List<string>()
        {
            "Λάδια","Φίλτρο Λαδιού","Φίλτρο","Φίλτρο Αέρος","Μπουζί","Φίλτρο καμπίνας","Βαλβολίνες","Φρένα Εμπρός","Φρένα Πίσω","Ιμάντας Χρονισμού","Ιμάντας Δυναμό","Υγρά Φρένων"
        };
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
            var sr = SelectedServices.ToList();
            Service newItem = new Service()
            {
                Id = Guid.NewGuid().ToString(),
                Changes = sr.Count.ToString(),
                Price = Price,
                Ser = sr.Select(x => x.ToString()).ToList()
            };
            await ServiceData.AddItemAsync(newItem);
            

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
