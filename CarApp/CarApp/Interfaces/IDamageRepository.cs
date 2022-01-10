using CarApp.Models;
using Microcharts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarApp.Interfaces
{
    public interface IDamageRepository : IRepository<Damage>
    {
        Task<List<ChartEntry>> ChartEntries(int year);
    }
}