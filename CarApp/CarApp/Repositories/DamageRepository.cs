using CarApp.Interfaces;
using CarApp.Models;
using SQLite;

namespace CarApp.Repositories
{
    public class DamageRepository: Repository<Damage>, IDamageRepository
    {
        private readonly SQLiteAsyncConnection _connection;
        public DamageRepository(SQLiteAsyncConnection db) : base(db)
        {
            _connection = db;
        }
    }
}
