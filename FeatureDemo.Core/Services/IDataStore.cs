using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FeatureDemo.Core.Models;
using TK.CustomMap;

namespace FeatureDemo.Core
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        Task<IEnumerable<TKCustomMapPin>> GetAtmsAsync();
        Task<TKCustomMapPin> GetAtm(string id);
    }
}
