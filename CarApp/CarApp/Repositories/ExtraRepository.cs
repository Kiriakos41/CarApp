using CarApp.Interfaces;
using CarApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace CarApp.Repositories
{
    public class ExtraRepository : Repository<Extra>, IExtraRepository
    {
        private readonly SQLiteAsyncConnection _connection;
        public ExtraRepository(SQLiteAsyncConnection db) : base(db)
        {
            _connection = db;
        }
    }
}
