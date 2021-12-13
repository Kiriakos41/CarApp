using CarApp.Interfaces;
using CarApp.Models;
using Microcharts;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SkiaSharp;
using System.Globalization;
using System;

namespace CarApp.Repositories
{
    public class AboutCarRepository : Repository<AboutCar>, IAboutCarRepository
    {
        private readonly SQLiteAsyncConnection _connection;
        public AboutCarRepository(SQLiteAsyncConnection db) : base(db)
        {
            _connection = db;
        }

        public async Task<List<ChartEntry>> ChartEntries(int year)
        {
            List<ChartEntry> entries = new List<ChartEntry>();
            List<string> colors = new List<string>
        {
            "#FF5733",
            "#33FF9F",
            "#3F33FF",
            "#AFFF33",
            "#FF3333",
            "#F700FF",
            "#FF007C",
            "#02FFCD",
            "#C70039",
            "#FFC300",
            "#FFC300",
            "#832C00",
            "#130C00",
        };
            var result = await _connection.Table<AboutCar>().ToListAsync();

            var perDate = result
                .Where(m => m.Date.Year == year)
                .Select(m => new { m.Date.Year, m.Date.Month, m.Price, price = m.Price })
                .GroupBy(m => new { m.Year, m.Month }, (key, group) => new
                {
                    SumPrice = group.Sum(k => k.Price),
                    Date = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(key.Month),
                    Year = key.Year,
                    Month = key.Month,
                })
                .OrderBy(m => m.Month)
                .ToList();

            var i = 0;
            foreach (var item in perDate)
            {
                i++;
                entries.Add(new ChartEntry((float)item.SumPrice)
                {
                    ValueLabel = item.SumPrice.ToString(),
                    Label = item.Date,
                    Color = SKColor.Parse(colors[i]),
                });
            }

            return entries;
        }
    }
}
