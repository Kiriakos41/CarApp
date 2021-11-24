using CarApp.Interfaces;
using CarApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Repositories
{
    public class ServiceTableRepository : Repository<Service>, IServiceTableRepository
    {
        public ServiceTableRepository(SQLiteAsyncConnection db) : base(db)
        {

        }
    }
}
