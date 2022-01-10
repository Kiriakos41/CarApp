using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Models
{
    public class Distance
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }

        public decimal CarDistance { get; set; }

        public DateTime Date { get; set; }
    }
}
