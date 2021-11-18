using CarApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    public class NewCleanViewModel : BaseViewModel
    {
        private string quality;
        private string price;

        public List<string> Rating { get; set; } = new List<string>()
        {
            "Απλή","Μέτρια","Τέλεια"
        };
        public NewCleanViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public string Quality
        {
            get => quality;
            set => SetProperty(ref quality, value);
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
            Clean newItem = new Clean()
            {
                Id = Guid.NewGuid().ToString(),
                Quality = Quality,
                Price = Price,
            };

            await CleanData.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
