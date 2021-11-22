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
                new AboutCar { Id = Guid.NewGuid().ToString(), Kilometer= 120, Gas="120", Price = "120", Date = new DateTime(2020,04,08) },
                new AboutCar { Id = Guid.NewGuid().ToString(), Kilometer= 120, Gas="120", Price = "120", Date = new DateTime(2018,08,06) },
                new AboutCar { Id = Guid.NewGuid().ToString(), Kilometer= 120, Gas="120", Price = "120", Date = new DateTime(2018,03,04) },
                new AboutCar { Id = Guid.NewGuid().ToString(), Kilometer= 120, Gas="120", Price = "120", Date = new DateTime(2020,02,02) },
                new AboutCar { Id = Guid.NewGuid().ToString(), Kilometer= 120, Gas="120", Price = "120", Date = new DateTime(2015,08,09) }
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