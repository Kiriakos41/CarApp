using CarApp.Interfaces;
using CarApp.Models;
using SQLite;

namespace CarApp.Repositories
{
    public class DistanceRepository : Repository<Distance> ,  IDistanceRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public DistanceRepository(SQLiteAsyncConnection db) : base(db)
        {
            _connection = db;
        }
    }
}
