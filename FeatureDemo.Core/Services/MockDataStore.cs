using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeatureDemo.Core.Models;
using TK.CustomMap;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;

namespace FeatureDemo.Core
{
    public class MockDataStore : IDataStore<Item>
    {
        //bool isInitialized;
        List<Item> items;
        List<TKCustomMapPin> atms;

        public MockDataStore()
        {
            items = new List<Item>();
            var _items = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is a nice description"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is a nice description"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is a nice description"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is a nice description"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is a nice description"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is a nice description"},
            };

            var geocoder = new Geocoder();
            atms = new List<TKCustomMapPin>();
            var _atms = new List<TKCustomMapPin>
            {
                new TKCustomMapPin { ID = "GTW", IsVisible = true, Title = "Gateway St, Springfield",  Subtitle = "3660 Gateway St, Springfield, OR 97477"},
                new TKCustomMapPin { ID = "MAN", IsVisible = true, Title = "Main St, Springfield", Subtitle = "5000 Main St, Springfield, OR 97478" },
                new TKCustomMapPin { ID = "W11", IsVisible = true, Title = "West 11th, Eugene", Subtitle = "3701 W 11th Ave, Eugene, OR 97402" },
                new TKCustomMapPin { ID = "DTE", IsVisible = true, Title = "East 8th, Eugene", Subtitle = "545 E 8th Ave, Eugene, OR 97401"}, 
                new TKCustomMapPin { ID = "SSR", IsVisible = true, Title = "Stephens St, Roseburg", Subtitle = "4221 NE Stephens St Suite 101, Roseburg, OR 97470"},
                new TKCustomMapPin { ID = "MOC", IsVisible = true, Title = "Molalla, Oregon City", Subtitle = "1689 Molalla Ave, Oregon City, OR 97045"}
            };

            foreach (Item item in _items)
            {
                items.Add(item);
            }
			foreach (TKCustomMapPin atm in _atms)
			{
                atm.Position = geocoder.GetPositionsForAddressAsync(atm.Subtitle).Result.FirstOrDefault();
				atms.Add(atm);
			}

        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            if (_item == null)
                return await Task.FromResult(false);

            var index = items.IndexOf(_item);
            items.Remove(_item);
            items.Insert(index, item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var _item = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<IEnumerable<TKCustomMapPin>> GetAtmsAsync()
        {
            return await Task.FromResult(atms);
        }

        public async Task<TKCustomMapPin> GetAtm(string id)
        {
            return await Task.FromResult(atms.FirstOrDefault(i => i.ID == id));
        }
    }
}
