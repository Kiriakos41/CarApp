using CarApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Services
{
    public class CarData : IDataStore<MyCar>
    {
        List<MyCar> carStats { get; set; } = new List<MyCar>();

        public async Task<bool> AddItemAsync(MyCar item)
        {
            carStats.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = carStats.Where((MyCar arg) => arg.Id == id).FirstOrDefault();
            carStats.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<MyCar> GetItemAsync(string id)
        {
            return await Task.FromResult(carStats.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<MyCar>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(carStats);
        }

        public async Task<bool> UpdateItemAsync(MyCar item)
        {
            var oldItem = carStats.Where(arg => arg.Id == item.Id).FirstOrDefault();
            carStats.Remove(oldItem);
            carStats.Add(item);

            return await Task.FromResult(true);
        }
    }
}