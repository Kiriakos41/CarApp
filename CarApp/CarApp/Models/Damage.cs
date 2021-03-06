using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Models
{
    public class Damage
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
