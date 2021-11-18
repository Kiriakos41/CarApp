using CarApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CarApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}