using CarApp.Interfaces;
using CarApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Repositories
{
    public class AboutCarRepository : Repository<AboutCar>, IAboutCarRepository
    {
        public AboutCarRepository(SQLiteAsyncConnection db) : base(db)
        {

        }

    }
}
