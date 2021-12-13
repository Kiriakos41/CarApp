using CarApp.Models;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Interfaces
{
    public interface IRubberRepository : IRepository<Rubber>
    {
        Task<List<ChartEntry>> ChartEntries(int year);
    }
}
