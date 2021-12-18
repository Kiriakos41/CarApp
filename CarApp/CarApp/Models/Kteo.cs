using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Models
{
    public class Kteo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string KteoDetail { get; set; }
        public DateTime Date { get; set; }
        public DateTime NextKteo { get; set; }
    }
}
