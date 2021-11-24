using SQLite;
using System;

namespace CarApp.Models
{
    public class AboutCar
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public long Gas { get; set; }
        public long Kilometer { get; set; }
        public string Price { get; set; }
        public DateTime Date { get; set; }
    }
}