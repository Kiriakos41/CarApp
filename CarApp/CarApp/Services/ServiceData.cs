using CarApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Services
{
    public class ServiceData : IDataStore<Service>
    {
        List<Service> serviceStats { get; set; } = new List<Service>();
        List<string> serviceList { get; set; } = new List<string>();

        public ServiceData()
        {
            serviceStats = new List<Service>()
            {
                new Service{ Id= Guid.NewGuid().ToString(), Price="15",Changes="2" },
                new Service{ Id= Guid.NewGuid().ToString(), Price="0",Changes="1" },
                new Service{ Id= Guid.NewGuid().ToString(), Price="120",Changes="7" },
            };
        }
        public async Task<bool> AddItemAsync(Service item)
        {
            serviceStats.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = serviceStats.Where((Service arg) => arg.Id == id).FirstOrDefault();
            serviceStats.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Service> GetItemAsync(string id)
        {
            return await Task.FromResult(serviceStats.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Service>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(serviceStats);
        }

        public async Task<bool> UpdateItemAsync(Service item)
        {
            var oldItem = serviceStats.Where(arg => arg.Id == item.Id).FirstOrDefault();
            serviceStats.Remove(oldItem);
            serviceStats.Add(item);

            return await Task.FromResult(true);
        }
    }
}
