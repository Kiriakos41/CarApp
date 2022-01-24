using CarApp.Interfaces;
using CarApp.Models;
using SQLite;

namespace CarApp.Repositories
{
    public class ServiceTableRepository : Repository<Service>, IServiceTableRepository
    {
        private readonly SQLiteAsyncConnection _connection;
        public ServiceTableRepository(SQLiteAsyncConnection db) : base(db)
        {
            _connection = db;
        }
    }
}
