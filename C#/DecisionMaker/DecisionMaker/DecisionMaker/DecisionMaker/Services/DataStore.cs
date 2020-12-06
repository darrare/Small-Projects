using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DecisionMaker.Models;
using Newtonsoft.Json;

namespace DecisionMaker.Services
{
    public class DataStore : IDataStore<Item>
    {
        List<Item> items = new List<Item>();
        Random rand = new Random();

        public DataStore()
        {
            foreach (var item in App.Instance.Properties)
            {
                items.Add(JsonConvert.DeserializeObject<Item>((string)item.Value));
            }
        }

        public async Task<Item> GetRandomItem()
        {
            return await Task.Factory.StartNew(() => { return items[rand.Next(0, items.Count)]; });
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            item.Id = items.Count == 0 ? 0 : items.Max(t => t.Id) + 1;
            items.Add(item);

            App.Instance.Properties.Add(item.Id.ToString(), JsonConvert.SerializeObject(item));
            await App.Instance.SavePropertiesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            App.Instance.Properties[item.Id.ToString()] = JsonConvert.SerializeObject(item);
            await App.Instance.SavePropertiesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            App.Instance.Properties.Remove(id.ToString());
            await App.Instance.SavePropertiesAsync();

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}