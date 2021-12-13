using CarApp.Repositories;
using CarApp.Views;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using System.Linq;

namespace CarApp.ViewModels
{
    public class ChartViewModel : BaseViewModel
    {
        public List<ChartEntry> Entries { get; set; }
        private int getYear;
        private string selectedItem;
        public string SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }
        public int GetYear
        {
            get => getYear;
            set => SetProperty(ref getYear, value);
        }
        public Command CarCommand { get; set; }
        public Command StartSearching { get; set; }
        public List<string> Type { get; set; } = new List<string>()
        {
            "Ανεφοδιασμός",
            "Service",
            "Καθαριότητα",
            "Βλάβες",
            "Ελαστικά"
        };
        public ChartViewModel()
        {
            StartSearching = new Command(GetEntries);
            Entries = new List<ChartEntry>();
            GetYear = DateTime.Now.Year;
            selectedItem = "Ανεφοδιασμός";
            GetEntries();
        }
        public async void GetEntries()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                Entries.Clear();
                if (SelectedItem == "Service")
                {
                    var entries = await unitOfwork.ServiceTables.ChartEntries(GetYear);
                    if (entries == null)
                    {
                        return;
                    }
                    else
                    {
                        foreach (var entry in entries)
                        {
                            Entries.Add(entry);
                        }
                    }
                }
                if (SelectedItem == "Καθαριότητα")
                {
                    var entries = await unitOfwork.Cleans.ChartEntries(GetYear);
                    if (entries == null)
                    {
                        return;
                    }
                    else
                    {
                        foreach (var entry in entries)
                        {
                            Entries.Add(entry);
                        }
                    }
                }
                if (SelectedItem == "Βλάβες")
                {
                    var entries = await unitOfwork.Damages.ChartEntries(GetYear);
                    if (entries == null)
                    {
                        return;
                    }
                    else
                    {
                        foreach (var entry in entries)
                        {
                            Entries.Add(entry);
                        }
                    }
                }
                if (SelectedItem == "Ανεφοδιασμός")
                {
                    var entries = await unitOfwork.AboutCars.ChartEntries(GetYear);
                    if (entries == null)
                    {
                        return;
                    }
                    else
                    {
                        foreach (var entry in entries)
                        {
                            Entries.Add(entry);
                        }
                    }
                }
                if (SelectedItem == "Ελαστικά")
                {
                    var entries = await unitOfwork.RubberTables.ChartEntries(GetYear);
                    if (entries == null)
                    {
                        return;
                    }
                    else
                    {
                        foreach (var entry in entries)
                        {
                            Entries.Add(entry);
                        }
                    }
                }
                if (selectedItem == null)
                {
                    var entries = await unitOfwork.AboutCars.ChartEntries(GetYear);
                    foreach (var entry in entries)
                    {
                        Entries.Add(entry);
                    }
                }
            }
        }
    }
}
