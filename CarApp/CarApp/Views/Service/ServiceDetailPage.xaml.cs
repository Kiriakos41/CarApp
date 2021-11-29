﻿using CarApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServiceDetailPage : ContentPage
    {
        ServiceDetailViewModel viewmodel;
        public ServiceDetailPage()
        {
            InitializeComponent();
            BindingContext = viewmodel = new ServiceDetailViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewmodel.FillServices();
        }
    }
}