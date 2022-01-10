using CarApp.Interfaces;
using CarApp.Models;
using SQLite;

namespace CarApp.Repositories
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        private readonly SQLiteAsyncConnection _connection;
        public ProfileRepository(SQLiteAsyncConnection db) : base(db)
        {
            _connection = db;
        }
    }
}
