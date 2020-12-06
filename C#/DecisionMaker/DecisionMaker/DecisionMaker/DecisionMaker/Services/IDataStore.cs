using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DecisionMaker.Models;

namespace DecisionMaker.Services
{
    public interface IDataStore<T>
    {
        Task<Item> GetRandomItem();
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
