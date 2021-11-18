using CarApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Services
{
    public class CleanData : IDataStore<Clean>
    {
        List<Clean> cleanStats { get; set; } = new List<Clean>();
        public CleanData()
        {
            cleanStats = new List<Clean>()
            {
                new Clean() { Id=Guid.NewGuid().ToString() ,Quality = "Άριστη", Price="25"},
                new Clean() { Id=Guid.NewGuid().ToString() ,Quality = "Μέτρια", Price="5"},
                new Clean() { Id=Guid.NewGuid().ToString() ,Quality = "Απλή", Price="1"},
            };
        }
        public async Task<bool> AddItemAsync(Clean item)
        {
            cleanStats.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = cleanStats.Where((Clean arg) => arg.Id == id).FirstOrDefault();
            cleanStats.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Clean> GetItemAsync(string id)
        {
            return await Task.FromResult(cleanStats.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Clean>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(cleanStats);
        }

        public async Task<bool> UpdateItemAsync(Clean item)
        {
            var oldItem = cleanStats.Where(arg => arg.Id == item.Id).FirstOrDefault();
            cleanStats.Remove(oldItem);
            cleanStats.Add(item);

            return await Task.FromResult(true);
        }
    }
}
