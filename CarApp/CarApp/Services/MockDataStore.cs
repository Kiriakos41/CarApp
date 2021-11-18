using CarApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarApp.Services
{
    public class MockDataStore : IDataStore<AboutCar>
    {
        readonly List<AboutCar> items;
        public MockDataStore()
        {
            items = new List<AboutCar>()
            {
                new AboutCar { Id = Guid.NewGuid().ToString(), Gas = "10", Kilometer="25", Price ="15"},
                new AboutCar { Id = Guid.NewGuid().ToString(), Gas = "20", Kilometer="50", Price ="20" },
                new AboutCar { Id = Guid.NewGuid().ToString(), Gas = "30", Kilometer="80", Price ="25" },
            };
        }
        public async Task<bool> AddItemAsync(AboutCar item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }
        public async Task<bool> UpdateItemAsync(AboutCar item)
        {
            var oldItem = items.Where((AboutCar arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where(arg => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }
        public async Task<AboutCar> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }
        public async Task<IEnumerable<AboutCar>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}