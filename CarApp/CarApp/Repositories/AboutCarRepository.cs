using CarApp.Interfaces;
using CarApp.Models;
using SQLite;

namespace CarApp.Repositories
{
    public class AboutCarRepository : Repository<AboutCar>, IAboutCarRepository
    {
        private readonly SQLiteAsyncConnection _connection;
        public AboutCarRepository(SQLiteAsyncConnection db) : base(db)
        {
            _connection = db;
        }
    }
}
