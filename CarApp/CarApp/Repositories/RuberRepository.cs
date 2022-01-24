using CarApp.Interfaces;
using CarApp.Models;
using SQLite;

namespace CarApp.Repositories
{
    public class RuberRepository : Repository<Rubber>, IRubberRepository
    {
        private readonly SQLiteAsyncConnection _connection;
        public RuberRepository(SQLiteAsyncConnection db) : base(db)
        {
            _connection = db;
        }
    }
}
