using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarApp.Models
{
    public class Service
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Price { get; set; }
        public string Changes { get; set; }
        public int NumberOfChanges { get; set; }
      
    }
}
