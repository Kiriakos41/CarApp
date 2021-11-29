using CarApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    public class ChartViewModel
    {
        public Command CarCommand { get; set; }
        public ChartViewModel()
        {
            CarCommand = new Command(GoToCarAsync);
        }


        public void GoToCarAsync()
        {
            Shell.Current.CurrentItem.CurrentItem.Items.Add(new CarPage());
            Shell.Current.CurrentItem.CurrentItem.Items.RemoveAt(0);
        }
    }
}
