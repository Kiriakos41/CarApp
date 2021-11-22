using CarApp.Interfaces;
using CarApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Repositories
{
    public class CleanRepository : Repository<Clean> , ICleanRepository
    {
        public CleanRepository(SQLiteAsyncConnection db) : base(db)
        {

        }
    }
}
