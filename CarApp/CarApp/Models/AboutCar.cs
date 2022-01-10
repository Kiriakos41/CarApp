using SQLite;
using System;

namespace CarApp.Models
{
    public class AboutCar
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public long Gas { get; set; }
        public string GasName { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}