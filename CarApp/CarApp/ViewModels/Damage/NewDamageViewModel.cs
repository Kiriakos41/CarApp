using CarApp.Models;
using CarApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CarApp.ViewModels
{
    public class NewDamageViewModel : BaseViewModel
    {
        private DateTime _date = DateTime.Now;
        private decimal price;
        private string desc;
        public List<string> Parts { get; set; } = new List<string> { "Φρένα", "Πόρτα", "Ιμάντας", "Φώτα", "Τούρμπο", "Μπαταρία", "Εξάτμιση", "Ζάντες","Μηχανή","Τιμόνι","ABS","Πιστόνια","Μπρστινό Τζάμι","Φιμέ","Κάθισματα","Ψυγείο","Ηχεία","΄Μοχλός Ταχυτήτων","Καπό","Καθρέφτης Πόρτας","Κοντέρ" };
        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public string Description
        {
            get => desc;
            set => SetProperty(ref desc, value);
        }
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }
        public NewDamageViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            Damage = new List<Damage>();
        }
        public Command AddEntryCommand { get; set; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public List<Damage> Damage { get; set; }
        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                Damage newItem = new Damage()
                {
                    Price = Price,
                    Description = Description,
                    Date = Date
                };

                Damage.Add(newItem);
                await unitOfwork.Damages.Insert(newItem);

                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
