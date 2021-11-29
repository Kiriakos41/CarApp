using CarApp.ViewModels;
using Microcharts;
using SkiaSharp;
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
    public partial class ChartsPage : ContentPage
    {
        ChartEntry[] entries = new[]
      {
            new ChartEntry(120  )
            {
                Label = "Ιανουάριος",
                ValueLabel = "120",
                Color = SKColor.Parse("#808080"),
            },
            new ChartEntry(200)
            {
                Label = "Φεβρουάριος",
                ValueLabel = "200",
                Color = SKColor.Parse("#d0191b"),
            },
            new ChartEntry(300)
            {
                Label = "Μάρτιος",
                ValueLabel = "300",
                Color = SKColor.Parse("#f06730"),
            },
            new ChartEntry(400)
            {
                Label = "Απρίλιος",
                ValueLabel = "400",
                Color = SKColor.Parse("#f08622"),
            },
            new ChartEntry(450)
            {
                Label = "Μάιος",
                ValueLabel = "420",
                Color = SKColor.Parse("#e9eb28"),
            },
            new ChartEntry(500)
            {
                Label = "Ιούνιος",
                ValueLabel = "500",
                Color = SKColor.Parse("#b4e742"),
            }
            , new ChartEntry(-100)
            {
                Label = "Ιούλιος",
                ValueLabel = "-100",
                Color = SKColor.Parse("#FFAC00"),
            }
            , new ChartEntry(-200)
            {
                Label = "Αύγουστος",
                ValueLabel = "-200",
                Color = SKColor.Parse("#118DFF"),
            },
             new ChartEntry(200)
            {
                Label = "Σεπτέμβριος",
                ValueLabel = "200",
                Color = SKColor.Parse("#8250C4"),
            },
              new ChartEntry(-150)
            {
                Label = "Οκτρώμβριος",
                ValueLabel = "-150",
                Color = SKColor.Parse("#D3004C"),
            },
               new ChartEntry(50)
            {
                Label = "Νοέμβριος",
                ValueLabel = "50",
                Color = SKColor.Parse("#002050"),
            },
                new ChartEntry(-200)
            {
                Label = "Δεκέμβριος",
                ValueLabel = "-200",
                Color = SKColor.Parse("#CCAA14"),
            },
        };
        public ChartsPage()
        {
            InitializeComponent();
            BindingContext = new ChartViewModel();
            chartViewBar.Chart = new PointChart { Entries = entries, LabelTextSize = 16 };
        }
    }
}