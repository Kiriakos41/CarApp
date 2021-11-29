using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Models
{
    public class Rubber
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Commend { get; set; }
        public DateTime Date { get; set; }
    }
}
