using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Models
{
    public class Clean
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Quality{ get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
