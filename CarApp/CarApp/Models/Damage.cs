using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Models
{
    public class Damage
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Prices { get; set; }
        public string Changes { get; set; }
    }
}
