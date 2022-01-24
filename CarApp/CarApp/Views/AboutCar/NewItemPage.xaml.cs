using CarApp.Models;
using CarApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarApp.Views
{
    public partial class NewItemPage : ContentPage
    {
        public AboutCar Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
            //test
        }
    }
}