using CarApp.Interfaces;
using CarApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Repositories
{
    public class RuberRepository : Repository<Rubber>, IRubberRepository
    {
        public RuberRepository(SQLiteAsyncConnection db) : base(db)
        {

        }
    }
}
