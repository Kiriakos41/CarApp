using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Models
{
    public class Extra
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
