using CarApp.Interfaces;
using CarApp.Models;
using Microcharts;
using SkiaSharp;
using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CarApp.Repositories
{
    public class DistanceRepository : Repository<Distance> ,  IDistanceRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public DistanceRepository(SQLiteAsyncConnection db) : base(db)
        {
            _connection = db;
        }
    }
}
