using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Models
{
    public class Damage
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }
        public string Price { get; set; }
    }
}
