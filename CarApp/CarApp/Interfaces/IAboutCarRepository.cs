using CarApp.Models;
using Microcharts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarApp.Interfaces
{
    public interface IAboutCarRepository : IRepository<AboutCar>
    {
        Task<List<ChartEntry>> ChartEntries(int year);
    }
}
