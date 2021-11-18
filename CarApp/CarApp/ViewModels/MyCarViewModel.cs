using CarApp.Models;
using CarApp.Services;
using CarApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace CarApp.ViewModels
{

    public class MyCarViewModel : BaseViewModel
    {
        private string id;
        private string type;
        private string horsePower;
        private string cubic;
        private MyCar car;

        public Command SaveCommad { get; set; }
        public Command CancelCommand { get; set; }

        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        public MyCarViewModel()
        {
            Title = "Tab1";
            SaveCommad = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
        }
        public MyCar Car
        {
            get => car;
            set => SetProperty(ref car, value);
        }
        public string Type
        {
            get => type;
            set
            {
                SetProperty(ref type, value);
            }
        }
        public string HorsePower
        {
            get => horsePower;
            set => SetProperty(ref horsePower, value);
        }
        public string CarCubic
        {
            get => cubic;
            set => SetProperty(ref cubic, value);
        }
        public async void OnAppear()
        {
            Car = await CarData.GetItemAsync("1");
            if (Car == null)
            {
                return;
            }
            else
            {
                Type = Car.Type;
                HorsePower = Car.HorsePower;
                CarCubic = Car.Cubic;
            }
        }
        public List<string> types { get; set; } = new List<string> { "Mazda", "Seat", "Polo", "Fiat", "Skoda", "Citroen", "Honda" };
        public List<string> Cubic { get; set; } = new List<string> { "800", "1000", "1200", "1400", "1500", "1600", "1800", "2000" };
        public List<string> power { get; set; } = new List<string> { "70", "80", "90", "100", "110", "120", "130", "140" };
        private async void OnSave()
        {
            MyCar car = new MyCar();
            car.Id = "1";
            car.Type = type;
            car.HorsePower = horsePower;
            car.Cubic = cubic;
            await CarData.AddItemAsync(car);
            await Shell.Current.GoToAsync(nameof(ItemsPage));
        }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync(nameof(ItemsPage));
        }
    }
}