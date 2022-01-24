using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Models
{
    public class Protection
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ProtectionName { get; set; }
        public decimal ProtectionPrice { get; set; }
        public DateTime ProtectionDate { get; set; }
    }
}
