using CarApp.Interfaces;
using CarApp.Models;
using SQLite;

namespace CarApp.Repositories
{
    public class ProtectionRepository : Repository<Protection>, IProtectionRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public ProtectionRepository(SQLiteAsyncConnection db) : base(db)
        {
            _connection = db;
        }
    }
}
