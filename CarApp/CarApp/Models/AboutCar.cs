using SQLite;
using System;

namespace CarApp.Models
{
    public class AboutCar
    {

        [PrimaryKey,AutoIncrement]
        public string Id { get; set; }
        public string Gas { get; set; }
        public long Kilometer { get; set; }
        public string Price { get; set; }
        public DateTime Date { get; set; }
    }
}