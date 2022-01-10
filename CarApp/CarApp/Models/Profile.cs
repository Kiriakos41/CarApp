using SQLite;

namespace CarApp.Models
{
    public class Profile
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string ImagePath { get; set; }
    }
}
