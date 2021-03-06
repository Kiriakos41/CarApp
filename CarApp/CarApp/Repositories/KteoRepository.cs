using CarApp.Interfaces;
using CarApp.Models;
using SQLite;

namespace CarApp.Repositories
{
    public class KteoRepository : Repository<Kteo>, IKteoRepository
    {
        private readonly SQLiteAsyncConnection _connection;
        public KteoRepository(SQLiteAsyncConnection db) : base(db)
        {
            _connection = db;
        }
    }
}
